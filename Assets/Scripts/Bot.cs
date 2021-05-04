using System;
using UnityEngine;

public class Bot : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationController animations; 
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageAmount = 1, maxHealth = 1;
    [SerializeField] private ParticleSystem smoke;
    private bool isFixed = false;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private int health;
    private Theme theme = Theme.Happy;
    private static readonly int
        LookX = Animator.StringToHash("Look X"),
        LookY = Animator.StringToHash("Look Y"),
        IsDefeated = Animator.StringToHash("Defeated"),
        IsDead = Animator.StringToHash("Dead");


    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.position = startPoint.position;
        anim = GetComponent<Animator>();
        theme = GameManager.Instance.theme;
        anim.runtimeAnimatorController = animations.Animators[(int)theme];
        Events.OnThemeChange += HandleThemeChange;
        
        health = maxHealth;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        theme = newTheme;
        anim.runtimeAnimatorController = animations.Animators[(int) theme];
    }

    private void FixedUpdate()
    {
        if (isFixed) return;
        Move();
    }

    private void Move()
    {
        var timeStamp = speed * Time.timeSinceLevelLoad;
        //Produce smoothly changing value between 0 and 1, and then back to 0, and so on.
        var pos = .5f + .5f * Mathf.Sin(timeStamp);
        //Lerping between start and end points using value above
        var startPosition = startPoint.position;
        var endPosition = endPoint.position;
        rigidbody2d.position = Vector2.Lerp(startPosition, endPosition, pos);
        //setting animation direction
        Vector2 direction = ((endPosition - startPosition) * Mathf.Cos(timeStamp)).normalized;
        //There are probably repeating calculations within lerp and direction. Might be possible to optimize
        anim.SetFloat(LookX, direction.x);
        anim.SetFloat(LookY, direction.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-damageAmount);
        }
    }

    public void TakeDamage(int value)
    {
        isFixed = true;
        anim.SetBool(IsDefeated, true);
        smoke.Stop();
    }

    public void OnDefeatedAnimationIsOver()
    {
        anim.SetBool(IsDead, true);
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
