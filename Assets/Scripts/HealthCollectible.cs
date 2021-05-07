using System;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healingAmount = 1;
    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<RubyController>();
        if (player == null) return;
        if (player.IsAtFullHealth) return;
        player.ChangeHealth(healingAmount);
        AudioSource.PlayClipAtPoint(aud.clip, AudioManager.Instance.transform.position, AudioManager.Instance.FXVolume);
        Destroy(gameObject);
    }
}
