using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField] private GroundController ground;
    [SerializeField] private Tilemap tmap;
    private Theme theme = Theme.Happy;

    private void Start()
    {
        theme = GameManager.Instance.theme;
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        for (int i = 0; i < ground.Sets[(int)theme].Tiles.Length; i++)
        {
            var tileFrom = ground.Sets[(int) theme].Tiles[i];
            var tileTo = ground.Sets[(int) newTheme].Tiles[i];
            if (tileTo != tileFrom)
                tmap.SwapTile(tileFrom, tileTo);
        }
        theme = newTheme;
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
