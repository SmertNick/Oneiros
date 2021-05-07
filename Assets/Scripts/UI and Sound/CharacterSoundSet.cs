using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/Character Sound Set")]
public class CharacterSoundSet : ScriptableObject
{
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip hit;

    public AudioClip Walk => walk;
    public AudioClip Attack => attack;
    public AudioClip Hit => hit;
}
