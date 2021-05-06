using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField] private GroundController ground;
    [SerializeField] private Tilemap tmap;
    private Theme theme = Theme.Happy;
    private TilemapCollider2D coll;

    private void Start()
    {
        theme = GameManager.Instance.theme;
        Events.OnThemeChange += HandleThemeChange;
        coll = tmap.gameObject.GetComponent<TilemapCollider2D>();
    }

    private void HandleThemeChange(Theme newTheme)
    {
        coll.enabled = false;
        for (int i = 0; i < ground.Sets[(int)theme].Tiles.Length; i++)
        {
            var tileFrom = ground.Sets[(int) theme].Tiles[i];
            var tileTo = ground.Sets[(int) newTheme].Tiles[i];
            if (tileTo != tileFrom)
                tmap.SwapTile(tileFrom, tileTo);
        }
        theme = newTheme;
        coll.enabled = true;
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
