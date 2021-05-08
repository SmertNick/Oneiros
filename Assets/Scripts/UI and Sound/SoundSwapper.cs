using UnityEngine;

public class SoundSwapper : MonoBehaviour
{
    [SerializeField] private SoundSet sounds;
    private AudioSource aud;

    
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = AudioManager.Instance.FXVolume;
        aud.clip = sounds.Set[(int) GameManager.Instance.theme];
        Events.OnFXVolumeChange += HandleVolumeChange;
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleVolumeChange(float sliderValue)
    {
        aud.volume = sliderValue;
    }
    
    private void HandleThemeChange(Theme newTheme)
    {
        if ((int) newTheme >= sounds.Set.Length) return;
        aud.clip = sounds.Set[(int) newTheme];
    }
    
    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
        Events.OnFXVolumeChange -= HandleVolumeChange;
    }
}
