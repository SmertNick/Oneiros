using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvases canvases;
    private GameObject mainMenu, pauseMenu, transitionAnimation;
    private readonly List<GameObject> huDs = new List<GameObject>();
    
    private void Start()
    {
        Events.OnGameStateChange += HandleGameStateChange;
        Events.OnThemeChange += HandleThemeChange;
        SetupCanvases();
    }

    private void SetupCanvases()
    {
        mainMenu = Instantiate(canvases.MainMenu, transform.position, Quaternion.identity);
        mainMenu.SetActive(true);

        pauseMenu = Instantiate(canvases.PauseMenu, transform.position, Quaternion.identity);
        pauseMenu.SetActive(false);
        
        transitionAnimation = Instantiate(canvases.TransitionScreen, transform.position, quaternion.identity);
        transitionAnimation.SetActive(false);
        
        foreach (GameObject hud in canvases.HUDCanvases)
        {
            huDs.Add(Instantiate(hud));
            hud.SetActive(false);
        }
    }

    private void HandleGameStateChange(GameState state, GameState previousState)
    {
        if (state == GameState.Running)
        {
            mainMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }

        if (state == GameState.Pregame)
        {
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    private void HandleThemeChange(Theme newTheme)
    {
        //TODO switch hud canvases
    }
    
    protected override void OnDestroy()
    {
        Events.OnGameStateChange -= HandleGameStateChange;
        Events.OnThemeChange -= HandleThemeChange;
        base.OnDestroy();
    }
}
