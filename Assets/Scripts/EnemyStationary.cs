using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : MonoBehaviour
{

    //private float shotDelay;
    //public float startShotDelay;

    public Transform player;
    //public GameObject projectile;

    public Transform bulletPoint;
    public bool isAttacking;
    public float orientation;
    public bool damaged;
    public BoxCollider2D hurtbox;

    public EnemyHexColor enemyHexColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(RepeatAttack());
        //shotDelay = startShotDelay;
    }

    void FixedUpdate()
    {
        // if(shotDelay <= 0){
        //     Instantiate(projectile, transform.position, Quaternion.identity);
        //     shotDelay = startShotDelay;
        // }else{
        //     shotDelay -= Time.deltaTime;
        // }
        orientation = transform.localScale.x;
        damaged = !hurtbox.enabled;

        if(damaged == false)
        {
            if((player.position.x < transform.position.x && orientation < 0) || (player.position.x > transform.position.x && orientation > 0))
            {
                transform.localScale *= new Vector2(-1, 1);
            } 

            if(Vector2.Distance(transform.position, player.position) < 6)
            {
                isAttacking = true;
            }else{
                isAttacking = false;
            }
        } else
        {
            StopAllCoroutines();
        }
    }

    void Attack()
    {       
        GameObject enemyBullet = ObjectPooler.SharedInstance.GetPooledObject(enemyHexColor.enemyColor.ToString());
        if (enemyBullet != null) {
            enemyBullet.transform.position = bulletPoint.position;
            enemyBullet.transform.parent = this.transform;
            enemyBullet.SetActive(true);
        }
    }
    IEnumerator RepeatAttack()
    {
        while (true)
        {
            if (isAttacking && damaged == false)
            {
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                Attack();
            }
            yield return new WaitForSeconds(0.75f);
        }
    }
}