using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    AnimationManager _animationManager;
    public string sceneName;

    void Start()
    {
        _animationManager = GetComponent<AnimationManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.SharedInstance.PlayClip("Teleport");
            _animationManager.ChangeAnimationState("HexTablet_Contacted");
            TransitionManager.SharedInstance.NextScene(new Color(0.6f, 0.4f, 1f), sceneName, 0.5f);
        }
    }
}
