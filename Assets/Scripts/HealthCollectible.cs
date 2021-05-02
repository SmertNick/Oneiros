using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healingAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<RubyController>();
        if (player == null) return;
        if (player.IsAtFullHealth) return;
        player.ChangeHealth(healingAmount);
        Destroy(gameObject);
    }
}
