using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    public float orientation;
    public float speed;
    public float distance;
    public bool movingLeft;
    public Transform groundDetect;
    public LayerMask groundLayer;
    public bool damaged;
    public BoxCollider2D hurtbox;
    public BoxCollider2D platform;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        movingLeft = true;
    }

    void FixedUpdate()
    {
        //transform.Translate(Vector2.left * speed * Time.deltaTime);

        orientation = transform.localScale.x;
        damaged = !hurtbox.enabled;
        
        if(damaged == false)
        {
            _rigidbody.velocity = new Vector2(speed * orientation, _rigidbody.velocity.y);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, distance, groundLayer);
            
            if (groundInfo.collider != platform)
            {
                if(movingLeft == true)
                {
                    //transform.eulerAngles = new Vector3(0, -180, 0);
                    transform.localScale *= new Vector2(-1, 1);
                    movingLeft = false;
                }else
                {
                    //transform.eulerAngles = new Vector3(0, 0, 0);
                    transform.localScale *= new Vector2(-1, 1);
                    movingLeft = true;
                }
            }
        }
    }
}
