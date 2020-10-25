using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Animator _animator;
    public BoxCollider2D hitbox;
    public BoxCollider2D hurtbox;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void HitByAttack()
    {
        _animator.SetTrigger("Death");
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        hitbox.enabled = false;
        hurtbox.enabled = false;
        Destroy(gameObject, 0.38f);
    }
}
