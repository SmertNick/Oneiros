using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Oneiros/Ground Tile Set")]
public class GroundTileSet : ScriptableObject
{
    [SerializeField] private Tile[] tiles;
    public Tile[] Tiles => tiles;
}

