using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/Sound Controller")]
public class SoundController : ScriptableObject
{
    [SerializeField] private SoundSet[] sets;
    public SoundSet[] Sets => sets;
}
