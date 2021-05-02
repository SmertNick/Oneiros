using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(menuName = "Oneiros/Ground Controller")]
public class GroundController : ScriptableObject
{
    [SerializeField] private GroundTileSet[] sets;
    public GroundTileSet[] Sets => sets;
}