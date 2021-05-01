public class AudioManager : Singleton<AudioManager>
{
    private void Start()
    {
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        
    }

    protected override void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
        base.OnDestroy();
    }
}
