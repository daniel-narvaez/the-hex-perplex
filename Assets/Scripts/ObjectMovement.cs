using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMovement : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public enum Direction {Vertical, Horizontal}
    public Direction _direction;
    public float moveSpeed, distance, rate;
    Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.freezeRotation = true;
    }
    void Update()
    {
        moveSpeed = distance / rate;
        switch(_direction)
        {
            case Direction.Vertical:
                _rigidbody2D.MovePosition(new Vector2(startPosition.x, Mathf.SmoothStep(startPosition.y - distance, startPosition.y + distance, Mathf.PingPong(Time.time * moveSpeed, 1))));
                break;
            case Direction.Horizontal:
                _rigidbody2D.MovePosition(new Vector2(Mathf.SmoothStep(startPosition.x - distance,startPosition.x + distance, Mathf.PingPong(Time.time * moveSpeed, 1)), startPosition.y));
                break;
        }
    }
}
