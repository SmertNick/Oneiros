using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private void Start()
    {
        Events.OnGameStateChange += HandleGameStateChange;
        Events.OnThemeChange += HandleThemeChange;
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
