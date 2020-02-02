using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance { get { return p_Instance; } }
    static protected GameManager p_Instance;

    [Header ("Game States")]
    public State[] States;
    private StateManager m_StateManager = new StateManager ();
    public StateManager stateManager { get { return m_StateManager; } }

    public string c_state = "";

    public bool isPaused = false;

    public float timeScale = 0;

    public float playerSpeed = 0f;

    private void Awake ()
    {
        if (p_Instance == null)
        {
            p_Instance = this;
            DontDestroyOnLoad (p_Instance);
        }

        if (States.Length <= 0)
        {
            Debug.LogWarning ("No states");
        }
        else
        {
            m_StateManager.Create (States);
        }

        // if (c_state != "")
        //     m_StateManager.SwitchState (c_state);
    }

    void Update ()
    {
        timeScale = Time.timeScale;

        m_StateManager.Tick ();
    }


    /* 
Note: iOS applications are usually suspended and do not quit. 
    If "Exit on Suspend" is not ticked then you will see calls to OnApplicationPause instead.
Note: On Windows Store Apps and Windows Phone 8.1 there is no application quit event. 
    Consider using OnApplicationFocus event when focusStatus equals false.
Note: On WebGL it is not possible to implement OnApplicationQuit due to nature of the browser tabs closing.
*/

    protected void OnApplicationQuit ()
    {
        // Debug.LogWarning ("Game Exit");
        // #if UNITY_ANALYTICS
        //         bool inGameExit = m_StateManager.stateStack[m_StateManager.stateStack.Count - 1].GetType () == typeof (GameState);

        //         Analytics.CustomEvent ("user_end_session", new Dictionary<string, object>
        //         { { "force_exit", inGameExit },
        //             { "timer", Time.realtimeSinceStartup }
        //         });
        // #endif
    }

    void OnApplicationFocus (bool hasFocus)
    {
        isPaused = !hasFocus;
        // Debug.LogWarning ("Game Focus");
    }

    void OnApplicationPause (bool pauseStatus)
    {
        isPaused = pauseStatus;
        // Debug.LogWarning ("Game Pause");
    }
}