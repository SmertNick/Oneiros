using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvases canvases;
    private GameObject MainMenu;
    private List<GameObject> HUDs = new List<GameObject>();
    
    private void Start()
    {
        Events.OnGameStateChange += HandleGameStateChange;
        Events.OnThemeChange += HandleThemeChange;
        SetupCanvases();
    }

    private void SetupCanvases()
    {
        MainMenu = Instantiate(canvases.MainMenu, transform.position, Quaternion.identity);
        MainMenu.SetActive(true);
        
        foreach (GameObject hud in canvases.HUDCanvases)
        {
            HUDs.Add(Instantiate(hud));
            hud.SetActive(false);
        }
    }

    private void HandleGameStateChange(GameState state, GameState previousState)
    {
        //pause menu
    }

    private void HandleThemeChange(Theme newTheme)
    {
        //switch canvases
    }
    
    protected override void OnDestroy()
    {
        Events.OnGameStateChange -= HandleGameStateChange;
        Events.OnThemeChange -= HandleThemeChange;
        base.OnDestroy();
    }
}
