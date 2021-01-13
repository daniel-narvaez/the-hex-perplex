using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    private TransitionManager _transitionManager;

    void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
    }

    //Title Screen Buttons
    public void GameStart()
    {
       // _transitionManager.NextScene(Color.black, "Level1");
    }

    public void Tutorial()
    {
        //_transitionManager.NextScene(Color.black, "Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
