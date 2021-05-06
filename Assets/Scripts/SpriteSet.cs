using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/SpriteSet")]
public class SpriteSet : ScriptableObject
{
    [SerializeField] private Sprite[] set;
    public Sprite[] Set => set;
}
