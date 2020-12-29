using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseDash : MonoBehaviour
{
    public CircleCollider2D[] _circleCollider2Ds;
    GameObject player;
    public float lifeSpan;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (CircleCollider2D c in _circleCollider2Ds)
        {
            c.enabled = false;
        }        
    }
    void OnEnable()
    {
        Reset();
        StartCoroutine(Pulsate());
        StartCoroutine(LifeSpan());
    }

    void Reset()
    {
        _circleCollider2Ds[0].radius = 0.65f;
        _circleCollider2Ds[1].offset = new Vector2(-0.7f, 0f);
        _circleCollider2Ds[2].offset = new Vector2(0.7f, 0f);
        foreach (CircleCollider2D c in _circleCollider2Ds)
        {
            c.enabled = false;
        }
    }

    IEnumerator Pulsate()
    {
        _circleCollider2Ds[0].enabled = true;
        yield return new WaitForSeconds(0.167f);

        _circleCollider2Ds[0].radius = 0.325f;
        _circleCollider2Ds[1].enabled = true;
        _circleCollider2Ds[2].enabled = true;
        yield return new WaitForSeconds(0.083f);

        _circleCollider2Ds[1].offset = new Vector2(-1f, 0f);
        _circleCollider2Ds[2].offset = new Vector2(1f, 0f);
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);

        foreach (CircleCollider2D c in _circleCollider2Ds)
        {
            c.enabled = false;
        }
        this.gameObject.SetActive(false);
    }
}
