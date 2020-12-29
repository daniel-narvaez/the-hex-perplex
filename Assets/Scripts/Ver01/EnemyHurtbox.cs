using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    public EnemyDeath enemyDeath;
    public EnemyHexColor enemyHexColor;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (enemyHexColor.enemyColor)
        {
            case EnemyHexColor.Color.Neutral:
                enemyDeath.HitByAttack();
            break;
            case EnemyHexColor.Color.RedEnemy:
            if(other.gameObject.tag == ("FireBall"))
            {
                enemyDeath.HitByAttack();
            }
            break;
            case EnemyHexColor.Color.BlueEnemy:
            if(other.gameObject.tag == ("Icicle"))
            {                
                enemyDeath.HitByAttack();
            }
            break;
            case EnemyHexColor.Color.YellowEnemy:
            if(other.gameObject.tag == ("Lightning"))
            {               
                enemyDeath.HitByAttack();
            }
            break;                                
        }
    }
}
