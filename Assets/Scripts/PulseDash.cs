using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseDash : MonoBehaviour
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
        Pulsate();
        StartCoroutine(LifeSpan());
    }

    void Pulsate()
    {
        _polygonCollider2D.enabled = true;        
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);

        _polygonCollider2D.enabled = false;
        this.gameObject.SetActive(false);
    }
}
