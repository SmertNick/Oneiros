using UnityEngine;

public static class Events
{
    #region SoundOptionsChange
    public delegate void SliderChange(float sliderValue);
    public static event SliderChange OnMusicVolumeChange;
    public static event SliderChange OnFXVolumeChange;
    
    public static void ChangeMusicVolume(float value)
    {
        OnMusicVolumeChange?.Invoke(value);
    }
    public static void ChangeFXVolume(float value)
    {
        OnFXVolumeChange?.Invoke(value);
    }
    
    #endregion
    #region StateChange

    public delegate void GameStateDelegate(GameState newState, GameState previousState);
    public static event GameStateDelegate OnGameStateChange;

    public static void ChangeGameState(GameState newState, GameState previousState)
    {
        OnGameStateChange?.Invoke(newState, previousState);
    }
    
    #endregion
    #region ThemeChange
    public delegate void ThemeDelegate(Theme newTheme);
    public static event ThemeDelegate OnThemeChange;

    public static void ChangeTheme(Theme newTheme)
    {
        OnThemeChange?.Invoke(newTheme);
        Debug.Log("switched to " + newTheme);
    }
    #endregion
}