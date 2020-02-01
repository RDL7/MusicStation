using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : State
{
    public Canvas canvas;
    public GameObject pausePanel;

    void Start ()
    {

    }

    public override string GetName ()
    {
        return "Game";
    }

    public override void Enter (State from)
    {
        canvas.gameObject.SetActive (true);
        GameManager.instance.playerSpeed = 1f;
    }

    public override void Exit (State from)
    {
        canvas.gameObject.SetActive (false);
    }

    // Update loop
    public override void Tick ()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            GameManager.instance.playerSpeed = 0;
        }
    }

}