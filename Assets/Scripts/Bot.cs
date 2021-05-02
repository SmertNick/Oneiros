using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageAmount = 1, maxHealth = 1;
    [SerializeField] private ParticleSystem smoke;
    private bool isFixed = false;
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    private int health;
    private static readonly int X = Animator.StringToHash("x");
    private static readonly int Y = Animator.StringToHash("y");
    private static readonly int IsFixed = Animator.StringToHash("isFixed");

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.position = startPoint.position;
        anim = GetComponent<Animator>();
        health = maxHealth;
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
        anim.SetFloat(X, direction.x);
        anim.SetFloat(Y, direction.y);
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
        anim.SetBool(IsFixed, true);
        smoke.Stop();
    }
}
