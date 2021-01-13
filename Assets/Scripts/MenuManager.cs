using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public Transform firstMenu;
    MenuInput _menuInput;

    void Start()
    {
        Reset();
        _menuInput = GetComponent<MenuInput>();
        if(!SceneManager.GetActiveScene().name.Contains("Level"))
            firstMenu.gameObject.SetActive(true);

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        eventSystem.firstSelectedGameObject = firstMenu.GetChild(0).gameObject;
    }

    public void Reset()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void CallButtonAction(string actionName)
    {
        Invoke(actionName, 0f);
    }

    public void CallTransitionManager(string sceneName)
    {
        TransitionManager.SharedInstance.NextScene(Color.white, sceneName, 1f);
    }

    void TogglePauseMenu()
    {
        _menuInput.TogglePauseMenu();
    }
    
    void GoBack()
    {
        Reset();
        firstMenu.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(firstMenu.GetChild(0).gameObject);        
    }

    void QuitGame()
    {
        Debug.Log("Game is Quitted.");
        Application.Quit();
    }

    public void OpenNewMenu(string menuName)
    {
        Reset();
        Transform newMenu = transform.Find(menuName);
        newMenu.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(newMenu.GetChild(0).gameObject);
    }
}
