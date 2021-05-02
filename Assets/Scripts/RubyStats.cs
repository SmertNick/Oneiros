using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "RubyStats")]
public class RubyStats : ScriptableObject
{
    [SerializeField] private int maxHealth = 5, maxBullets = 20;
    [SerializeField] private float invincibilityTime = 3f;
    [SerializeField] private Vector2 speed;
    [SerializeField] private AudioClip throwCogSound;
    public GameObject bullet;
    public float InvincibilityTime => invincibilityTime;
    public Vector2 Speed => speed;
    public int MaxHealth => maxHealth;
    public int MaxBullets => maxBullets;
    public AudioClip ThrowCogSound => throwCogSound;
}
