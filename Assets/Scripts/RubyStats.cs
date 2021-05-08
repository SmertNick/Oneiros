using UnityEngine;

[CreateAssetMenu(menuName = "RubyStats")]
public class RubyStats : ScriptableObject
{
    [SerializeField] private int maxHealth = 5, maxBullets = 20;
    [SerializeField] private float invincibilityTime = 1f;
    [SerializeField] private float fireCooldownTime = 1f;
    [SerializeField] private Vector2 speed;
    public GameObject bullet;
    public float InvincibilityTime => invincibilityTime;
    public float FireCooldownTime => fireCooldownTime;
    public Vector2 Speed => speed;
    public int MaxHealth => maxHealth;
    public int MaxBullets => maxBullets;
}
