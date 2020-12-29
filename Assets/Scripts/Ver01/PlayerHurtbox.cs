using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = this.transform.parent.gameObject.GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(player.HitByAttack());
    }
}
