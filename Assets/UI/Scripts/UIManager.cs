using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    public Events.EventVolumeChange OnVolumeChanged;
    [SerializeField] private Slider fxVolume, musicVolume;
    
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
        fxVolume.onValueChanged.AddListener(ChangeVolume);
        musicVolume.onValueChanged.AddListener(ChangeVolume);
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleGameStateChange(GameState state, GameState previousState)
    {
        
    }

    private void HandleThemeChange(Theme newTheme)
    {
        //switch canvases
    }
    
    private void ChangeVolume(float f)
    {
        OnVolumeChanged.Invoke(musicVolume, fxVolume);
    }

    protected override void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
        base.OnDestroy();
    }
}
