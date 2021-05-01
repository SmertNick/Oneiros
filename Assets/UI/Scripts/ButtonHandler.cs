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
        aud.Stop();
        aud.clip = soundController.Sets[(int) theme].ButtonHoverSound;
        aud.Play();
    }

    private void PlayClickSound()
    {
        aud.Stop();
        aud.clip = soundController.Sets[(int) theme].ButtonClickSound;
        aud.Play();
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
