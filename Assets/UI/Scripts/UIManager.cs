using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider fxVolume, musicVolume;
    
    private void Start()
    {
        fxVolume.onValueChanged.AddListener(Events.ChangeFXVolume);
        musicVolume.onValueChanged.AddListener(Events.ChangeMusicVolume);
        Events.OnThemeChange += HandleThemeChange;
        Events.OnGameStateChange += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState state, GameState previousState)
    {
        
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
