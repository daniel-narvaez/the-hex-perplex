using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    public float _velocity = -600;
    public float _lifeSpan = 1f;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
        StartCoroutine(LifeSpan());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.25f);
        _rigidbody.AddForce(new Vector2(0, _velocity));
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(_lifeSpan);
        _rigidbody.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
}
