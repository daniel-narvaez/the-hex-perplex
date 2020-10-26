using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    public float _velocity = 400;
    public float _lifeSpan = 2;
    float orientation;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        orientation = Player.orientation;
        if(orientation < 0) 
        {transform.localScale = new Vector2(-1, transform.localScale.y);} else
        {transform.localScale = new Vector2(1, transform.localScale.y);}
        StartCoroutine(Shoot());
        StartCoroutine(LifeSpan());
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
        transform.localScale = new Vector2(transform.localScale.x * orientation, transform.localScale.y); 
        _rigidbody.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
}
