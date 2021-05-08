﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton, optionsButton, restartButton, quitButton;
    [SerializeField] private GameObject pauseMenu, optionsMenu;

    private void Awake()
    {
        Events.OnGameStateChange += HandleStateChange;
        resumeButton.onClick.AddListener(ResumeGame);
        optionsButton.onClick.AddListener(Options);
        restartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(Quit);

        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void HandleStateChange(GameState newState, GameState previousState)
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(newState == GameState.Paused);
    }

    private void ResumeGame()
    {
        GameManager.Instance.TogglePause();
    }

    private void Options()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    private void Restart()
    {
        throw new NotImplementedException();
    }

    private void Quit()
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        Events.OnGameStateChange -= HandleStateChange;
        resumeButton.onClick.RemoveListener(ResumeGame);
        optionsButton.onClick.RemoveListener(Options);
        restartButton.onClick.RemoveListener(Restart);
        quitButton.onClick.RemoveListener(Quit);
    }
}
