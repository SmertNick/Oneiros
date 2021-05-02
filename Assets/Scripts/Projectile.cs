using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rbody;
    [SerializeField] private float force = 300f;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rbody.AddForce(transform.right * force, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //sparks animation
        gameObject.SetActive(false);
        if (other.gameObject.GetComponent<IDamageable>() == null) return;
        other.gameObject.GetComponent<IDamageable>().TakeDamage(1);
    }
}
