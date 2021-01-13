using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : Hurtbox
{
    BoxCollider2D _boxCollider2D;
    ParticleSystem _particleSystem;
    public override void Start()
    {
        base.Start();
        _particleSystem = GetComponent<ParticleSystem>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override IEnumerator Death()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animationManager.ChangeAnimationState("Player_Dying_Non_Int");
        _particleSystem.Play();
        _boxCollider2D.enabled = false;
        AudioManager.SharedInstance.PlayClip("PlayerDeath");
        yield return new WaitForSeconds(0.5f);

        parent.SetActive(false);
        TransitionManager.SharedInstance.RestartLevel();
    }
}
