using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioSource aud;
    [SerializeField] private AudioClip backgroundClip;
    private float baseVolume = .2f, baseFontSize = 45f;
    [SerializeField] private TMP_Text[] options;
    [SerializeField] private Button startGameButton, optionsButton, creditsButton, quitButton;
    [SerializeField] private GameObject optionsMenu, creditsMenu;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        if (aud != null)
        {
            baseVolume = aud.volume;
            if (backgroundClip != null)
            {
                aud.clip = backgroundClip;
            }
        }
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        Events.OnMusicVolumeChange += ChangeVolume;
        startGameButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(Options);
        creditsButton.onClick.AddListener(Credits);
        quitButton.onClick.AddListener(Quit);
    }

    private void ChangeVolume(float value)
    {
        if (aud != null)
        {
            aud.volume = baseVolume * value;
        }
    }

    private void OnDestroy()
    {
        Events.OnMusicVolumeChange -= ChangeVolume;
    }

    private void ChangeFont(Slider fontSizeSlider, Slider colorSlider)
    {
        foreach(TMP_Text text in options)
        {
            text.fontSize = baseFontSize * fontSizeSlider.value;
        }
    }

    private void StartGame()
    {

    }

    private void Options()
    {
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }
    private void Credits()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(!creditsMenu.activeInHierarchy);
    }

    private void Quit()
    {
            
    }
}
