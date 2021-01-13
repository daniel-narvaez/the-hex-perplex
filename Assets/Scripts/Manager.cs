using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Manager : MonoBehaviour
{
    public virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public virtual void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        Reset();
    }

    public abstract void Reset();
}
