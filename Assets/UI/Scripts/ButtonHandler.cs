using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    private Button button;
    private AudioSource aud;
    [SerializeField] private AudioClip hoverClip, clickClip;
    private float baseVolume = .2f, baseFontSize = 60f;
    private TMP_Text text;

    private void Start()
    {
        button = GetComponent<Button>();
        aud = GetComponent<AudioSource>();
        button.onClick.AddListener(PlayClickSound);
        if (aud != null)
        {
            baseVolume = aud.volume;
        }
        if (button != null)
        {
            text = button.GetComponentInChildren<TMP_Text>();
            if (text != null)
            {
                baseFontSize = text.fontSize;
            }
        }
        Events.OnFXVolumeChange += ChangeVolume;
    }
    public void OnHover()
    {
        if (button != null && aud != null && hoverClip != null)
        {
            aud.Stop();
            aud.clip = hoverClip;
            aud.Play();
        }
    }

    private void PlayClickSound()
    {
        if (button != null && aud != null && clickClip != null)
        {
            aud.Stop();
            aud.clip = clickClip;
            aud.Play();
        }
    }

    private void ChangeVolume(float value)
    {
        if (aud != null)
        {
            aud.volume = baseVolume * value;
        }
    }

    private void ChangeFont(Slider fontSize, Slider color)
    {
        if (text != null)
        {
            text.enableAutoSizing = false;
            text.fontSize = baseFontSize*fontSize.value;
        }
    }

    private void OnDestroy()
    {
        Events.OnFXVolumeChange -= ChangeVolume;
    }
}
