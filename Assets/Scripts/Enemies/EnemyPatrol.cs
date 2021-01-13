using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy
{
    public GameObject platform;
    Vector2 platformSize;
    BoxCollider2D platformCollider;
    public GameObject detectionPoint;
    public float orientation, moveSpeed;
    public LayerMask groundLayer, intruderLayer;

    public override void Start()
    {
        base.Start();
        RaycastHit2D platformSearch = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, 1f, groundLayer);
        platform = platformSearch.collider.gameObject;
        platformSize = platform.GetComponent<SpriteRenderer>().size;
        platformCollider = platform.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        bool alert = Physics2D.BoxCast(platform.transform.position, platformSize, 0f, Vector2.up, 1, intruderLayer);

        switch(alert)
        {
            case (false):
                currentState = State.Idle;
                _animationManager.ChangeAnimationState("Enemy_Idle");
                moveSpeed = -1.5f;
                break;
            case (true):
                currentState = State.Alert;
                _animationManager.ChangeAnimationState("Enemy_Alert");
                moveSpeed = -3f;
                // if((intruder.position.x > transform.position.x && orientation > 0) || (intruder.position.x < transform.position.x && orientation < 0))

                break;
        }

        orientation = transform.localScale.x;
        _rigidbody2D.velocity = new Vector2(moveSpeed * orientation, _rigidbody2D.velocity.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, 1f, groundLayer);
            
        if (groundInfo.collider != platformCollider)
        {
            transform.localScale *= new Vector2(-1, 1);
        }
        
    }
    public override void CreatePoint()
    {
        detectionPoint = new GameObject("DetectionPoint");
        detectionPoint.transform.parent = this.transform;
        detectionPoint.transform.localPosition = new Vector2(-0.75f, -0.75f);
    }
}
