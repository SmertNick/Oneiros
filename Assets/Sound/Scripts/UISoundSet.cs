using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/UI Sound Set")]
public class UISoundSet : ScriptableObject
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip buttonHoverSound;
    [SerializeField] private AudioClip buttonClickSound;

    public AudioClip BackgroundMusic => backgroundMusic;
    public AudioClip ButtonHoverSound => buttonHoverSound;
    public AudioClip ButtonClickSound => buttonClickSound;
}
