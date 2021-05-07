using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private UISoundController soundController;
    private readonly List<AudioSource> sources = new List<AudioSource>();
    private float musicVolume = .5f, fxVolume = .5f;
    public float MusicVolume => musicVolume;
    public float FXVolume => fxVolume;

    protected override void Awake()
    {
        base.Awake();
        Events.OnThemeChange += HandleThemeChange;
        Events.OnGameStateChange += HandleStateChange;
        Events.OnMusicVolumeChange += ChangeMusicVolume;
        Events.OnFXVolumeChange += ChangeFXVolume;
        SetUpSound();
    }

    private void ChangeFXVolume(float value)
    {
        fxVolume = value;
    }

    private void ChangeMusicVolume(float value)
    {
        musicVolume = value;
        foreach (var aud in sources)
        {
            aud.volume = value;
        }
    }

    private void SetUpSound()
    {
        foreach (var set in soundController.Sets)
        {
            AudioSource aud = gameObject.AddComponent<AudioSource>();
            sources.Add(aud);
            aud.playOnAwake = false;
            aud.Stop();
            aud.clip = set.BackgroundMusic;
            aud.volume = musicVolume;
            aud.mute = true;
            aud.loop = true;
        }
    }

    private void HandleStateChange(GameState newState, GameState previousState)
    {
        switch (newState)
        {
            case GameState.Running:
            {
                foreach (var aud in sources)
                    aud.Play();
                break;
            }
            case GameState.Paused:
            {
                foreach (var aud in sources)
                    aud.Pause();
                break;
            }
            case GameState.Pregame:
            {
                foreach (var aud in sources)
                    aud.Stop();
                break;
            }
        }
    }

    private void HandleThemeChange(Theme newTheme)
    {
        for (int i = 0; i < sources.Count; i++)
            sources[i].mute = i != (int) newTheme;
    }

    protected override void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
        Events.OnGameStateChange -= HandleStateChange;
        Events.OnMusicVolumeChange -= ChangeMusicVolume;
        base.OnDestroy();
    }
}
