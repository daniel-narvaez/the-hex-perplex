using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    GameObject leftBoundary, rightBoundary;

    void Start()
    {
        leftBoundary = GameObject.Find("LeftBoundary");
        rightBoundary = GameObject.Find("RightBoundary");
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftBoundary.transform.position.x + 1, rightBoundary.transform.position.x - 1), transform.position.y);
    }
}
