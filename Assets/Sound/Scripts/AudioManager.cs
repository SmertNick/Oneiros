using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource[] sources;
    [SerializeField] private UISoundController soundController;

    private void Start()
    {
        Events.OnThemeChange += HandleThemeChange;
        Events.OnGameStateChange += HandleStateChange;
        SetUpSound();
    }

    private void SetUpSound()
    {
        for (int i = 0; i < soundController.Sets.Length; i++)
        {
            sources[i].Stop();
            sources[i].clip = soundController.Sets[i].BackgroundMusic;
            sources[i].volume = .5f;
            sources[i].mute = true;
            sources[i].loop = true;
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
        for (int i = 0; i < sources.Length; i++)
            sources[i].mute = i != (int) newTheme;
    }

    protected override void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
        base.OnDestroy();
    }
}
