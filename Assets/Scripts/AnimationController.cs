using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/Animation Controller")]
public class AnimationController : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController[] animators;
    [SerializeField] private Sprite[] sprites;

    public RuntimeAnimatorController[] Animators => animators;
    public Sprite[] Sprites => sprites;
}
