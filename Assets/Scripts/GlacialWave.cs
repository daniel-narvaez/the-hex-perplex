using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacialWave : MonoBehaviour
{
    PolygonCollider2D _polygonCollider2D;
    GameObject player;
    public float lifeSpan;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _polygonCollider2D.enabled = false;
    }

    void OnEnable()
    {
        StartCoroutine(Shoot());
        StartCoroutine(LifeSpan());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.33f);

        _polygonCollider2D.enabled = true;
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);

        transform.parent = null;
        _polygonCollider2D.enabled = false;
        this.gameObject.SetActive(false);
    }
}
