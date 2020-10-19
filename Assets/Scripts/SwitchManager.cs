using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager SharedInstance;
    public bool blocksActivated;
    public bool redPathActivated;
    public bool bluePathActivated;
    public bool yellowPathActivated;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        blocksActivated = false;
        redPathActivated = false;
        bluePathActivated = false;
        yellowPathActivated = false;
    }
}
