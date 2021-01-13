using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    CircleCollider2D range;
    GameObject spawnPoint;
    Transform intruder;
    bool alert;
    public float attackDelay;
    public float orientation;
    public enum Direction {Vertical, Horizontal} //The option for this enemy to move horizontally is there, but NORMALLY, its meant to be vertical
    public Direction _direction;
    public float moveSpeed;
    public float distance;
    Vector2 startPosition;

    public override void Start()
    {        
        base.Start();
        
        range = GetComponent<CircleCollider2D>();
        StartCoroutine(RepeatAttack());
        startPosition = transform.position;
    }

    void Update()
    {
        orientation = transform.localScale.x;
        moveSpeed = distance / 10;
        switch(currentState)
        {
            case (State.Idle):
                _animationManager.ChangeAnimationState("Enemy_Idle");
                alert = false;
                range.radius = 4f;
                break;
            case (State.Alert):
                _animationManager.ChangeAnimationState("Enemy_Alert");
                alert = true;
                range.radius = 7f;
                if((intruder.position.x > transform.position.x && orientation > 0) || (intruder.position.x < transform.position.x && orientation < 0))
                    transform.localScale *= new Vector2(-1, 1);
                break;
        }

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

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentState = State.Alert;
            intruder = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentState = State.Idle;
            intruder = null;
        }
    }

    public override void CreatePoint()
    {
        spawnPoint = new GameObject("SpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(-0.046f, 0.14f);
    }

    public override IEnumerator InitiateAttack()
    {
        AudioManager.SharedInstance.PlayClip("EnemyAttack1");
        GameObject powerBeam = ObjectPooler.SharedInstance.GetPooledObject("EnemyAttack1");
        if (powerBeam != null)
        {
            powerBeam.transform.position = spawnPoint.transform.position;
            powerBeam.transform.rotation = Quaternion.LookRotation(Vector3.forward, powerBeam.transform.position - intruder.position); //to aim
            powerBeam.SetActive(true);

            powerBeam.transform.localScale = new Vector2(1, 1);
        }
        yield return null;
    }

    public override IEnumerator RepeatAttack()
    {
        yield return new WaitForSeconds(1);
        while(true)
        {
            yield return new WaitForSeconds(attackDelay);
            if(alert)
                StartCoroutine(InitiateAttack());
        }
    }
}
