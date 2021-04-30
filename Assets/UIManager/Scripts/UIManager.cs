using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    public Events.EventVolumeChange OnVolumeChanged;
    public Events.EventFontChange OnFontChanged;
    [SerializeField] private Slider FXVolume, musicVolume, fontSize;
    
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
        FXVolume.onValueChanged.AddListener(ChangeVolume);
        musicVolume.onValueChanged.AddListener(ChangeVolume);
        fontSize.onValueChanged.AddListener(ChangeFont);
    }

    private void HandleGameStateChange(GameManager.GameState state, GameManager.GameState previousState)
    {
        
    }
    
    private void ChangeVolume(float f)
    {
        OnVolumeChanged.Invoke(musicVolume, FXVolume);
    }

    private void ChangeFont(float f)
    {
        OnFontChanged.Invoke(fontSize, fontSize);
    }
}
