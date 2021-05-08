using UnityEngine;

public class Bot : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationController animations; 
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageAmount = 1, maxHealth = 1;
    [SerializeField] private ParticleSystem smoke;
    private bool isDefeated = false;
    private bool isDead = false;
    private AudioSource aud;
    private Collider2D coll; 
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private int health;
    private Vector2 direction;
    private Theme theme = Theme.Happy;
    private static readonly int
        LookX = Animator.StringToHash("Look X"),
        LookY = Animator.StringToHash("Look Y"),
        IsDefeated = Animator.StringToHash("Defeated"),
        IsDead = Animator.StringToHash("Dead");


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.position = startPoint.position;
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        health = maxHealth;

        theme = GameManager.Instance.theme;
        anim.runtimeAnimatorController = animations.Animators[(int)theme];
        Events.OnThemeChange += HandleThemeChange;
    }

    private void HandleThemeChange(Theme newTheme)
    {
        theme = newTheme;
        anim.runtimeAnimatorController = animations.Animators[(int) theme];
        anim.SetFloat(LookX, direction.x);
        anim.SetFloat(LookY, direction.y);
        anim.SetBool(IsDefeated, isDefeated);
        anim.SetBool(IsDead, isDead);
    }

    private void FixedUpdate()
    {
        if (isDefeated) return;
        Move();
    }

    private void Move()
    {
        //TODO use Mathf.PingPong instead?
        var timeStamp = speed * Time.timeSinceLevelLoad;
        //Produce smoothly changing value between 0 and 1, and then back to 0, and so on.
        var pos = .5f + .5f * Mathf.Sin(timeStamp);
        //Lerping between start and end points using value above
        var startPosition = startPoint.position;
        var endPosition = endPoint.position;
        rigidbody2d.position = Vector2.Lerp(startPosition, endPosition, pos);
        //setting animation direction
        direction = ((endPosition - startPosition) * Mathf.Cos(timeStamp)).normalized;
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
        isDefeated = true;
        anim.SetBool(IsDefeated, true);
        smoke.Stop(); //particles are off atm anyway
        aud.PlayOneShot(aud.clip, AudioManager.Instance.FXVolume);
        //"turning off" collider.
        //disabling collider2D (not 3D though) component for some reason turns off entire gameobject
        coll.isTrigger = true;
    }

    public void OnDefeatedAnimationIsOver()
    {
        anim.SetBool(IsDead, true);
        isDead = true;
    }

    private void OnDestroy()
    {
        Events.OnThemeChange -= HandleThemeChange;
    }
}
