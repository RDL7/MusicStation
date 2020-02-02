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
        isPaused = GameManager.instance.isPaused;

        canvas.gameObject.SetActive (true);
        pausePanel.SetActive (false);
        comboPanel.SetActive (false);
        GameManager.instance.playerSpeed = 5f;
        levelManager.RestartAll ();
    }

    public override void Exit (State from)
    {
        canvas.gameObject.SetActive (false);
        pausePanel.SetActive (false);

    }

    public override void Tick ()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            GamePause ();
        }
    }

    public void GamePause ()
    {
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
        GameManager.instance.isPaused = false;
        GameManager.instance.stateManager.SwitchState ("Menu");
    }

    public void GameQuit ()
    {
        Application.Quit ();
    }

    public void GameOverEvent ()
    {
        GameManager.instance.playerSpeed = 0;
        GameManager.instance.stateManager.SwitchState ("Game Over");
    }

}