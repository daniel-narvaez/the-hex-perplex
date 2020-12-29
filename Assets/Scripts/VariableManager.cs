using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static VariableManager _variableManager;


    //Movement
    public float gravity = 2;

    void Awake()
    {
        _variableManager = this;
    }
}
