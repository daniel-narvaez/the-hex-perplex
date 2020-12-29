using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroShot : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    GameObject player;
    public float launchForce;
    public float lifeSpan;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        StartCoroutine(Launch());
        StartCoroutine(LifeSpan());
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     this.gameObject.SetActive(false);
    // }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(0.75f);

        transform.parent = null;
        _rigidbody2D.velocity = new Vector2(launchForce * transform.localScale.x, 0);
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);

        _rigidbody2D.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
}
