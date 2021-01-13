using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : Hurtbox
{
    Enemy _enemy;

    public override void Start()
    {
        base.Start();
        _enemy = parent.GetComponent<Enemy>();
    }

    public override IEnumerator Death()
    {
        AudioManager.SharedInstance.PlayClip("EnemyDeath");
        _enemy.StopAllCoroutines();
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animationManager.ChangeAnimationState("Enemy_Death_Non_Int");
        yield return new WaitForSeconds(0.375f);

        parent.SetActive(false);
    }
}
