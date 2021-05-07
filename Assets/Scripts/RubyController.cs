using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField] private RubyStats stats;
    [SerializeField] private AnimationController animations;
    [SerializeField] private CharacterSoundController sounds;
    private Rigidbody2D body;
    private Animator anim;
    private int health, bulletsRemaining;
    private bool isInvincible = false, justFired = false;
    private AudioSource aud;
    public bool IsAtFullHealth => (health >= stats.MaxHealth);

    private static readonly int 
        Speed = Animator.StringToHash("Speed"),
        LookX = Animator.StringToHash("Look X"),
        LookY = Animator.StringToHash("Look Y"),
        Hit = Animator.StringToHash("Hit"),
        Attack = Animator.StringToHash("Attack");
    private readonly List<GameObject> bullets = new List<GameObject>();
    private Vector2 move, direction = new Vector2(0f, -1f);
    [SerializeField] private Transform bulletSpawnPosition;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        aud.volume = AudioManager.Instance.FXVolume;
        health = stats.MaxHealth;
        bulletsRemaining = stats.MaxBullets;
        GenerateBullets(20);
        Events.OnThemeChange += HandleThemeChange;
        Events.OnFXVolumeChange += HandleVolumeChange;
    }

    private void HandleVolumeChange(float sliderValue)
    {
        aud.volume = sliderValue;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        anim.runtimeAnimatorController = animations.Animators[(int) newTheme];
        anim.SetFloat(LookX, direction.x);
        anim.SetFloat(LookY, direction.y);
        anim.SetFloat(Speed, move.magnitude);
    }

    private void GenerateBullets(int amountOfBullets)
    {
        for (int i = 0; i < amountOfBullets; i++)
        {
            GameObject projectile = Instantiate(stats.bullet);
            projectile.SetActive(false);
            bullets.Add(projectile);
        }
    }

    private void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetButtonDown("Jump"))
            FireBullet();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void FireBullet()
    {
        if (bulletsRemaining <= 0) return;
        if (justFired) return;
        StartCoroutine(FireCooldown());
        foreach (var bullet in bullets)
        {
            if (bullet.activeInHierarchy) continue;
            bullet.transform.position = bulletSpawnPosition.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.transform.Rotate(Vector3.forward, Vector2.SignedAngle(Vector2.right, direction));
            bullet.SetActive(true);
            anim.SetTrigger(Attack);
            aud.PlayOneShot(sounds.Sets[(int) GameManager.Instance.theme].Attack, AudioManager.Instance.FXVolume);
            bulletsRemaining--;
            return;
        }
    }

    private IEnumerator FireCooldown()
    {
        justFired = true;
        yield return new WaitForSeconds(stats.FireCooldownTime);
        justFired = false;
    }


    private void Move()
    {
        anim.SetFloat(Speed, move.magnitude);
        body.velocity = move * stats.Speed;

        if (Mathf.Approximately((move.magnitude), 0f)) return;
        direction = move;
        anim.SetFloat(LookX, direction.x);
        anim.SetFloat(LookY, direction.y);
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(stats.InvincibilityTime);
        isInvincible = false;
    } 

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            StartCoroutine(Invincibility());
            anim.SetTrigger(Hit);
            aud.PlayOneShot(sounds.Sets[(int) GameManager.Instance.theme].Hit);
        }
        health = Mathf.Clamp(health + amount, 0, stats.MaxHealth);
        Debug.Log($"{health}/{stats.MaxHealth}");
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
        Events.OnFXVolumeChange -= HandleVolumeChange;
    }
}