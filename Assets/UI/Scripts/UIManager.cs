using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvases canvases;
    private GameObject mainMenu;
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
        
        foreach (GameObject hud in canvases.HUDCanvases)
        {
            huDs.Add(Instantiate(hud));
            hud.SetActive(false);
        }
    }

    private void HandleGameStateChange(GameState state, GameState previousState)
    {
        //pause menu pregame->game : turn on hud
    }

    private void HandleThemeChange(Theme newTheme)
    {
        //switch hud canvases
    }
    
    protected override void OnDestroy()
    {
        Events.OnGameStateChange -= HandleGameStateChange;
        Events.OnThemeChange -= HandleThemeChange;
        base.OnDestroy();
    }
}
