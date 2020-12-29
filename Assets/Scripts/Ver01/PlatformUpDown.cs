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

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("ContactPoint"))
    //     {
    //         other.transform.parent.transform.SetParent(transform);
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("ContactPoint"))
    //     {
    //         other.transform.parent.transform.SetParent(null);
    //     }
    // }
}
