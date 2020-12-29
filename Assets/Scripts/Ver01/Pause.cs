using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public static bool isPaused;
	public GameObject PauseMenu;
	public PlayerControls controls;
	private TransitionManager _transitionManager;

	void Awake()
	{
		controls = new PlayerControls();
		controls.Pause.Pause.performed += ctx => TogglePauseMenu();
	}

	void OnEnable()
	{
		controls.Pause.Enable();
		PauseMenu.SetActive(false);
		isPaused = false;

		_transitionManager = FindObjectOfType<TransitionManager>();
	}

	void OnDisable()
	{
		controls.Pause.Disable();
	}
	
	void TogglePauseMenu()
	{
		isPaused = !isPaused;

		if (isPaused) {
			PauseMenu.SetActive (true);
			Time.timeScale = 0f;
		} else 
		{
			PauseMenu.SetActive (false);
			Time.timeScale = 1f;
		}
	}

	public void Resume()
	{
		TogglePauseMenu();
	}

	public void ReturnToTitle()
	{
		Time.timeScale = 1f;
		_transitionManager.NextScene("Title");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}