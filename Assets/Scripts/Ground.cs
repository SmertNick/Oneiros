using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField] private GroundController ground;
    [SerializeField] private Tilemap tmap;
    private Theme theme = Theme.Happy;
    private Tilemap tmap2;
    

    private void Start()
    {
        theme = GameManager.Instance.theme;
        Events.OnThemeChange += HandleThemeChange;
        SetUpTileMaps();
    }


    private void SetUpTileMaps()
    {
        var groundObj = tmap.gameObject;
        tmap2 = Instantiate(groundObj, transform.position, Quaternion.identity, groundObj.transform.parent).GetComponent<Tilemap>();
        tmap2.gameObject.SetActive(false);
        for (int i = 0; i < ground.Sets[0].Tiles.Length; i++)
        {
            var tileFrom = ground.Sets[0].Tiles[i];
            var tileTo = ground.Sets[1].Tiles[i];
            if (tileTo != tileFrom)
                tmap2.SwapTile(tileFrom, tileTo);
        }
    }

    private void HandleThemeChange(Theme newTheme)
    {
        //говнокод. Заменить нах
        if (newTheme == Theme.Brutal)
        {
            tmap.gameObject.SetActive(false);
            tmap2.gameObject.SetActive(true);
        }
        else
        {
            tmap2.gameObject.SetActive(false);
            tmap.gameObject.SetActive(true);
        }
        theme = newTheme;
    }
    
    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
