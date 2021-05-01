using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioSource aud;
    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private Button startGameButton, optionsButton, creditsButton, quitButton;
    [SerializeField] private GameObject optionsMenu, creditsMenu;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        if (aud != null && backgroundClip != null)
            aud.clip = backgroundClip;
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
