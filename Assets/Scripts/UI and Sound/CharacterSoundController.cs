using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/Sound Controller")]
public class CharacterSoundController : ScriptableObject
{
    [SerializeField] private CharacterSoundSet[] sets;
    public CharacterSoundSet[] Sets => sets;
}
