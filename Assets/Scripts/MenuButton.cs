using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public MenuManager _menuManager;
    Button _button;
    Text buttonText;
    public string buttonName;
    public string actionName;
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TaskOnClick);
        buttonText = GetComponent<Text>();
        buttonText.text = buttonName;
        this.gameObject.name = buttonName;
    }

    public void TaskOnClick()
    {
        AudioManager.SharedInstance.PlayClip("MenuButton");
    }

    public void PressButton()
    {
        _menuManager.CallButtonAction(actionName);
    }

    public void OpenNewMenu()
    {
        _menuManager.OpenNewMenu(actionName);
    }

    public void StartLevel()
    {
        Time.timeScale = 1f;
        _menuManager.CallTransitionManager(actionName);
    }
}
