using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchManager : Manager
{
    public static SwitchManager SharedInstance;
    public bool redPathActivated, bluePathActivated, yellowPathActivated; //Set during gameplay
    public bool redPathStart, bluePathStart, yellowPathStart; //Set in inspector


    void Awake()
    {
        if (FindObjectsOfType<SwitchManager>().Length > 1)
            Destroy(gameObject);

        if(SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Reset();
    }

    public override void Reset()
    {
        redPathActivated = redPathStart;
        bluePathActivated = bluePathStart;
        yellowPathActivated = yellowPathStart;
    }

    public void SetEachHexPath(bool redState, bool blueState, bool yellowState)
    {
        redPathStart = redState;
        bluePathStart = blueState;
        yellowPathStart = yellowState;
        Reset();
    }

    public void SetAllHexPaths(bool state)
    {
        redPathActivated = state;
        bluePathActivated = state;
        yellowPathActivated = state;
    }
}
