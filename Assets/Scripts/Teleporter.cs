using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private TransitionManager _transitionManager;
    public string sceneName;

    void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _transitionManager.NextScene(sceneName);
        }
    }
}
