using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager
{
    static public StateManager instance { get { return pInstance; } }
    static protected StateManager pInstance;

    public List<State> stateStack { get { return pStateStack; } }
    protected List<State> pStateStack = new List<State>();
    protected Dictionary<string, State> pStateDictionary = new Dictionary<string, State>();

    public int stackCount { get { return stateStack.Count; } }

    // Create State Mashine and settings
    public void Create(State[] _states)
    {
        if (pInstance == null)
        {
            pInstance = this;
        }

        // No duplicates
        pStateDictionary.Clear();

        for (int stateId = 0; stateId < _states.Length; stateId++)
        {
            _states[stateId].m_StateManager = this;
            pStateDictionary.Add(_states[stateId].GetName(), _states[stateId]);
        }

        // Add first state in stack
        AddState(_states[0].GetName());
    }

    public void Tick() {
        if (stackCount > 0)
        {
            stateStack[stackCount - 1].Tick ();
        }
    }

    private void AddState(string _stateName)
    {
        State state = FindState(_stateName);

        if (pStateStack.Count > 0)
        {
            pStateStack[pStateStack.Count - 1].Exit(state);
            state.Enter(pStateStack[pStateStack.Count - 1]);
        }
        else
        {
            state.Enter(null);
        }
        pStateStack.Add(state);
    }

    public State FindState(string stateName)
    {
        State state;
        if (!pStateDictionary.TryGetValue(stateName, out state))
        {
            Debug.LogError("No such state");
            return null;
        }

        return state;
    }

    public State CurrentState()
    {
        State state;
        state = stateStack[stackCount - 1];

        return state;
    }

    public void SwitchState(string newState)
    {
        State state = FindState(newState);
        if (state == null)
        {
            Debug.LogError("Can't find the state named " + newState);
            return;
        }

        pStateStack[pStateStack.Count - 1].Exit(state);
        state.Enter(pStateStack[pStateStack.Count - 1]);
        pStateStack.RemoveAt(pStateStack.Count - 1);
        pStateStack.Add(state);
    }
}

public abstract class State : MonoBehaviour
{
    [HideInInspector]
    public StateManager m_StateManager;

    public abstract void Enter(State from);
    public abstract void Exit(State to);
    public abstract void Tick();

    public abstract string GetName();
}