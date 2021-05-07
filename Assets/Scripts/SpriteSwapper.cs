using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private SpriteSet sprites;
    private SpriteRenderer sRenderer;
    
    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        if ((int) newTheme >= sprites.Set.Length) return;
        sRenderer.sprite = sprites.Set[(int) newTheme];
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
