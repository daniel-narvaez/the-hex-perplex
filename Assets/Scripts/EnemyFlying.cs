using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    public enum Direction {UpDown, LeftRight}
    public Direction _direction;
    public float speed;
    public float distance;
    private float startPosition;

    void Start()
    {        
        switch(_direction)
        {
            case Direction.UpDown:
            startPosition = transform.position.y;
            break;
            case Direction.LeftRight:
            startPosition = transform.position.x;
            break;
        }
    }

    void Update()
    {
        Vector2 newPosition = transform.position;
        switch(_direction)
        {
            case Direction.UpDown:
            newPosition.y = Mathf.SmoothStep(startPosition, startPosition + distance, Mathf.PingPong(Time.time * speed, 1));
            break;
            case Direction.LeftRight:
            newPosition.x = Mathf.SmoothStep(startPosition, startPosition + distance, Mathf.PingPong(Time.time * speed, 1));
            break;
        }
        transform.position = newPosition;
    }
}
