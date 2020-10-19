using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    public float _velocity = 400;
    public float _lifeSpan = 2;
    public float orientation;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
        StartCoroutine(LifeSpan());
        orientation = Player.orientation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        _rigidbody.AddForce(new Vector2(_velocity * orientation, 0));
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(_lifeSpan);
        _rigidbody.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
}
