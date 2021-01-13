using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBeam : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public float beamForce = 400;
    public float lifeSpan;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();        
    }
    void OnEnable()
    {
        Shoot();
        StartCoroutine(LifeSpan());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }

    void Shoot()
    {
        _rigidbody2D.AddForce(-transform.up * beamForce);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        _rigidbody2D.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
}
