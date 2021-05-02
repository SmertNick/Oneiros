using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<RubyController>();
        if (player == null) return;
        player.ChangeHealth(-damageAmount);
    }
}
