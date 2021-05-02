using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioSource aud;
    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private Button startGameButton, optionsButton, creditsButton, quitButton;
    [SerializeField] private GameObject optionsMenu, creditsMenu;
    [SerializeField] private Image happyBackground, brutalBackground;
    private Color colorFull, colorClear;
    [SerializeField] private TMP_Text s, phrenia;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        if (aud != null)
        {
            aud.volume = AudioManager.Instance.MusicVolume;
            aud.clip = backgroundClip;
        }

        colorClear = new Color(1f, 1f, 1f, 0f);
        colorFull = Color.white;
        brutalBackground.color = colorClear;

        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        Events.OnMusicVolumeChange += ChangeVolume;
        startGameButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(Options);
        creditsButton.onClick.AddListener(Credits);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        float tmp = .4f * Mathf.PingPong(Time.timeSinceLevelLoad * .3f, 1f);
        brutalBackground.color = Color.Lerp(colorClear, colorFull, tmp);
        s.color = Color.Lerp(colorClear, colorFull, 1f - tmp);
        phrenia.color = Color.Lerp(colorClear, colorFull, tmp);
    }

    private void ChangeVolume(float value)
    {
        if (aud != null)
        {
            aud.volume = value;
        }
    }

    private void OnDestroy()
    {
        Events.OnMusicVolumeChange -= ChangeVolume;
        startGameButton.onClick.RemoveListener(StartGame);
        optionsButton.onClick.RemoveListener(Options);
        creditsButton.onClick.RemoveListener(Credits);
        quitButton.onClick.RemoveListener(Quit);
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
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
        GameManager.Instance.QuitGame();
    }
}
