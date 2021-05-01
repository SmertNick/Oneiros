using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    private Button button;
    private AudioSource aud;
    [SerializeField] private AudioClip hoverClip, clickClip;
    private TMP_Text text;

    private void Start()
    {
        button = GetComponent<Button>();
        aud = GetComponent<AudioSource>();
        button.onClick.AddListener(PlayClickSound);
        Events.OnFXVolumeChange += ChangeVolume;
    }
    public void OnHover()
    {
        if (button == null || aud == null || hoverClip == null) return;
        aud.Stop();
        aud.clip = hoverClip;
        aud.Play();
    }

    private void PlayClickSound()
    {
        if (button == null || aud == null || clickClip == null) return;
        aud.Stop();
        aud.clip = clickClip;
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
    }
}
