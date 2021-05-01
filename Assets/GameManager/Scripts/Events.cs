using System;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
    [System.Serializable] public class EventVolumeChange: UnityEvent<UnityEngine.UI.Slider, UnityEngine.UI.Slider> { }

    #region ThemeChange
    public delegate void ThemeDelegate(Theme newTheme);
    public static event ThemeDelegate OnThemeChange;

    public void ChangeTheme(Theme newTheme)
    {
        OnThemeChange?.Invoke(newTheme);
    }


    public void ChangeTheme(int themeId)
    {
        OnThemeChange?.Invoke((Theme)themeId);
    }

    #endregion

    public delegate void SliderChange(float sliderValue);
}
