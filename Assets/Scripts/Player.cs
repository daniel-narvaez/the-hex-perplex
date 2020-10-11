using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   

    //For this project, I think it's best if we use the new input system, so that we can link
    //player controls to code much easier. The new input system creates a namespace (InputSystem).
    //I've gone ahead and created controls for movement.

    //This right here is a reference to the Input Action Asset "PlayerControls"
    PlayerControls controls;
    
    Rigidbody2D _rigidbody;

    void Awake()
    {
        controls = new PlayerControls();
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
