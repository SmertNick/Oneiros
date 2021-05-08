using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private Button button;
    private AudioSource aud;
    private Theme theme = Theme.Happy;
    [SerializeField] private UISoundController soundController;

    private void Start()
    {
        button = GetComponent<Button>();
        aud = GetComponent<AudioSource>();
        aud.volume = AudioManager.Instance.FXVolume;
        theme = GameManager.Instance.theme;
        button.onClick.AddListener(PlayClickSound);
        Events.OnFXVolumeChange += ChangeVolume;
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        theme = newTheme;
    }

    public void OnHover()
    {
        aud.PlayOneShot(soundController.Sets[(int) theme].ButtonHoverSound, aud.volume);
    }

    private void PlayClickSound()
    {
        aud.PlayOneShot(soundController.Sets[(int) theme].ButtonClickSound, aud.volume);
    }

    private void ChangeVolume(float value)
    {
        if (aud != null)
            aud.volume = value;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(PlayClickSound);
        Events.OnFXVolumeChange -= ChangeVolume;
        Events.OnThemeChange -= HandleThemeChange;
    }
}
