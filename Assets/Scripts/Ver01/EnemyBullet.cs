using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    public float _velocity = 400;
    public float _lifeSpan = 2;
    float orientation;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        orientation = transform.GetComponentInParent<EnemyStationary>().orientation;
        transform.localScale = new Vector2(transform.localScale.x * orientation, transform.localScale.y);
        Shoot();
        StartCoroutine(LifeSpan());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }

    void Shoot()
    {
        _rigidbody.AddForce(new Vector2(_velocity * orientation, 0));
        transform.parent = null;
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(_lifeSpan);
        _rigidbody.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
}
