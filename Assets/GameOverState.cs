using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : State
{
    public Canvas canvas;
    public LevelManager levelManager;

    public override string GetName ()
    {
        return "Game Over";
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

}