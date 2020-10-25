using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private TransitionManager _transitionManager;

    void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
    }
    public void GameStart()
    {
        
    }

    public void Tutorial()
    {
        _transitionManager.NextScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
