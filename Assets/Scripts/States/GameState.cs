using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : State
{
    public Canvas canvas;
    public GameObject pausePanel;

    bool isPaused = false;

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
        GameManager.instance.playerSpeed = 5f;
    }

    public override void Exit (State from)
    {
        canvas.gameObject.SetActive (false);
    }

    // Update loop
    public override void Tick ()
    {

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            GamePause ();
        }
    }

    public void GamePause ()
    {
        // TODO: Cache player speed
        if (!isPaused)
        {
            isPaused = true;
            pausePanel.SetActive (true);
            GameManager.instance.playerSpeed = 0;
        } else {
            isPaused = false;
            pausePanel.SetActive (false);
            GameManager.instance.playerSpeed = 1;
        }
    }

    public void GameMenu() {
        GameManager.instance.stateManager.SwitchState("Menu");
    }

    public void GameQuit() {
        // GameManager.instance.stateManager.SwitchState("Menu");
        Application.Quit();
        Debug.Log("Quit");
    }

}