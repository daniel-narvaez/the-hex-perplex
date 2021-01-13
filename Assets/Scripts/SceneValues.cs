using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneValues : MonoBehaviour
{
    public string newSong;
    public bool redState, blueState, yellowState;
    void Start()
    {
        if(AudioManager.SharedInstance != null)    
            AudioManager.SharedInstance.PlaySong(newSong);
        
        if(SwitchManager.SharedInstance != null)
            SwitchManager.SharedInstance.SetEachHexPath(redState, blueState, yellowState);
    }
}
