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
        sRenderer.sprite = sprites.Set[(int) newTheme];
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
