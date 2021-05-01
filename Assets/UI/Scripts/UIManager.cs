using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject[] HUD;
    
    private void Start()
    {
        Events.OnGameStateChange += HandleGameStateChange;
        Events.OnThemeChange += HandleThemeChange;
        Instantiate(MainMenu, transform.position, Quaternion.identity);
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
