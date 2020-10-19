using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    //LateUpdate let's the camera focus on its target based on the current frame and not the last one.
    void LateUpdate()
    {
        // Vector3 pos = transform.position;
        // pos.x = player.position.x;  
        // transform.position = pos;
        
        if(player.position.y >= 0)
        {
            Vector3 pos = transform.position;
            pos.y = player.position.y;  
            transform.position = pos;
        }
    }
}
