using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : Enemy
{
    CircleCollider2D range;
    GameObject spawnPoint;
    Transform intruder;
    bool alert;
    public int attackRate;
    public float attackDelay;
    public float orientation;

    public override void Start()
    {
        base.Start();
        
        range = GetComponent<CircleCollider2D>();
        StartCoroutine(RepeatAttack());
    }

    void Update()
    {
        orientation = transform.localScale.x;
        switch(currentState)
        {
            case (State.Idle):
                _animationManager.ChangeAnimationState("Enemy_Idle");
                alert = false;
                break;
            case (State.Alert):
                _animationManager.ChangeAnimationState("Enemy_Alert");
                alert = true;
                if((intruder.position.x > transform.position.x && orientation > 0) || (intruder.position.x < transform.position.x && orientation < 0))
                    transform.localScale *= new Vector2(-1, 1);
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
        spawnPoint.transform.localPosition = new Vector2(-0.079f, 0.4525f);
    }

    public override IEnumerator InitiateAttack()
    {
        for(int i = 0; i < attackRate; i++)
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
            yield return new WaitForSeconds(0.05f);
        }
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