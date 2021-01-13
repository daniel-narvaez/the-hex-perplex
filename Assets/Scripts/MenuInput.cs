using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    public MenuManager _menuManager;
    public InputAction Pause, Back;
    public static bool isPaused;
    GameObject player;

    void Awake()
    {
        Pause.started += ctx => TogglePauseMenu();
    }

    void OnEnable()
    {
        Pause.Enable();
        Back.Enable();
    }

    void OnDisable()
    {
        Pause.Disable();
        Back.Disable();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        _menuManager = FindObjectOfType<MenuManager>();
        isPaused = false;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        switch(isPaused)
        {
            case true:
                player.SetActive(false);
                _menuManager.OpenNewMenu("Menu_Paused");
                Time.timeScale = 0f;
                break;
            case false:
			    Time.timeScale = 1f;
                player.SetActive(true);
                _menuManager.Reset();
            break;
        }
    }
}
