using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    public float speed;
    public float distance;
    private float startPosition;

    void Start()
    {
        startPosition = transform.position.y;
    }

    void Update()
    {
        Vector2 newPosition = transform.position;
        newPosition.y = Mathf.SmoothStep(startPosition, startPosition + distance, Mathf.PingPong(Time.time * speed, 1));
        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.tag.Contains("Enemy"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            other.transform.SetParent(null);
        }
    }
}
