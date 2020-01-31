using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : State
{
    public Canvas canvas;

    void Start ()
    {

    }

    public override string GetName ()
    {
        return "Menu";
    }

    public override void Enter (State from)
    {
        canvas.gameObject.SetActive (true);
    }

    public override void Exit (State from)
    {
        canvas.gameObject.SetActive (false);
    }

    // Update loop
    public override void Tick ()
    {

    }

    public void StartGame ()
    {
        GameManager.instance.stateManager.SwitchState("Game");
    }

}