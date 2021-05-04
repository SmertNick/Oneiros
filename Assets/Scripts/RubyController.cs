using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField] private RubyStats stats;
    [SerializeField] private AnimationController animations;
    private Rigidbody2D body;
    private Animator anim;
    private int health, bulletsRemainig;
    private bool isInvincible;
    private AudioSource aud;
    public bool IsAtFullHealth => (health >= stats.MaxHealth);

    private static readonly int 
        Speed = Animator.StringToHash("Speed"),
        LookX = Animator.StringToHash("Look X"),
        LookY = Animator.StringToHash("Look Y"),
        Attack = Animator.StringToHash("Attack");
    private readonly List<GameObject> bullets = new List<GameObject>();
    private Vector2 move, direction = new Vector2(0f, -1f);
    [SerializeField] private Transform bulletSpawnPosition;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        health = stats.MaxHealth;
        isInvincible = false;
        bulletsRemainig = stats.MaxBullets;
        GenerateBullets(20);
        Events.OnThemeChange += HandleThemeChange;
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
        if (bulletsRemainig <= 0) return;
        foreach (var bullet in bullets)
        {
            if (bullet.activeInHierarchy) continue;
            bullet.transform.position = bulletSpawnPosition.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.transform.Rotate(Vector3.forward, Vector2.SignedAngle(Vector2.right, direction));
            bullet.SetActive(true);
            anim.SetTrigger(Attack);
            aud.Stop();
            aud.clip = stats.ThrowCogSound;
            aud.Play();
            bulletsRemainig--;
            return;
        }
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
        }
        health = Mathf.Clamp(health + amount, 0, stats.MaxHealth);
        Debug.Log($"{health}/{stats.MaxHealth}");
    }
}