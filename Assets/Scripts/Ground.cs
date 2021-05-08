using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField] private GroundController ground;
    [SerializeField] private GameObject tileMap;
    private readonly List<GameObject> maps = new List<GameObject>();
    

    private void Start()
    {
        Events.OnThemeChange += HandleThemeChange;
        SetUpTileMaps();
    }

    private void SetUpTileMaps()
    {
        foreach (var set in ground.Sets)
        {
            //creating tilemap copies
            var obj = Instantiate(tileMap, transform.position, Quaternion.identity,
                tileMap.transform.parent);
            maps.Add(obj);
            var tmap = obj.GetComponent<Tilemap>();
            obj.SetActive(false);
            for (int i = 0; i < set.Tiles.Length; i++) //swapping tiles with their counterparts
            {
                var tileFrom = ground.Sets[0].Tiles[i];
                var tileTo = set.Tiles[i];
                if (tileTo != tileFrom)
                    tmap.SwapTile(tileFrom, tileTo);
            }
        }
        tileMap.SetActive(false);
        maps[(int)GameManager.Instance.theme].SetActive(true);
    }

    private void HandleThemeChange(Theme newTheme)
    {
        for (int i = 0; i < maps.Count; i++)
            maps[i].SetActive(i == (int) newTheme);
    }
    
    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
