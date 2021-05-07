using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/SoundSet")]
public class SoundSet : ScriptableObject
{
    [SerializeField] private AudioClip[] set;
    public AudioClip[] Set => set;
}
