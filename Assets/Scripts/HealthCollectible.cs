using System;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healingAmount = 1;
    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<RubyController>();
        if (player == null) return;
        if (player.IsAtFullHealth) return;
        player.ChangeHealth(healingAmount);
        aud.Play();
        Destroy(gameObject);
    }
}
