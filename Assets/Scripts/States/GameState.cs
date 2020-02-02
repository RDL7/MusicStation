using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : State
{
    public Canvas canvas;
    public LevelManager levelManager;

    public GameObject pausePanel;
    public GameObject comboPanel;

    private bool isPaused = false;

    private void Start ()
    {
        EventManager.OnGameOver += GameOverEvent;
    }
    public override string GetName ()
    {
        return "Game";
    }

    public override void Enter (State from)
    {
        levelManager.RestartAll ();
        canvas.gameObject.SetActive (true);
        pausePanel.SetActive (false);
        comboPanel.SetActive (false);
        GameManager.instance.playerSpeed = 5f;
        
        GameManager.instance.isPaused = false;
    }

    public override void Exit (State from)
    {
        canvas.gameObject.SetActive (false);
        pausePanel.SetActive (false);
        // comboPanel.SetActive (false);
        // GameManager.instance.playerSpeed = 5f;
        levelManager.RestartAll ();
        // GameManager.instance.isPaused = false;
    }

    // Update loop
    public override void Tick ()
    {

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            print ("escape");
            GamePause ();
        }

        // if (isPaused)
        // {
        //     comboPanel.SetActive (false);
        // }
    }

    public void GamePause ()
    {
        // TODO: Cache player speed
        if (!isPaused)
        {
            isPaused = true;

                pausePanel.SetActive (true);
                GameManager.instance.playerSpeed = 0;
            
        }
        else if (isPaused)
        {
            isPaused = false;
            pausePanel.SetActive (false);
            GameManager.instance.playerSpeed = 5;

            if (levelManager.showCombo)
            {
                comboPanel.SetActive (true);
            }
        }

        GameManager.instance.isPaused = isPaused;
    }

    public void GameMenu ()
    {
        GameManager.instance.stateManager.SwitchState ("Menu");
    }

    public void GameQuit ()
    {
        // GameManager.instance.stateManager.SwitchState("Menu");
        Application.Quit ();
        Debug.Log ("Quit");
    }

    public void GameOverEvent ()
    {
        GameManager.instance.playerSpeed = 0;
        GameManager.instance.stateManager.SwitchState ("Game Over");
    }

}