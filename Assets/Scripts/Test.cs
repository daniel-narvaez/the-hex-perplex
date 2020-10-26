using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{   
    [SerializeField]
    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Jump.performed += ctx => Log();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void Log()
    {
        Debug.Log("Testing");
    }
}
