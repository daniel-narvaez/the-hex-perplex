using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public GameObject faderCanvas;
    public Image faderImg;
    public float fadeTime;
    
    private void Awake()
    {
        if (FindObjectsOfType<TransitionManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            faderCanvas.SetActive(true);
            faderImg.canvasRenderer.SetAlpha(1);
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }    

    //Called by other scripts. Begins the fade out process
    public void NextScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(sceneName));
    }

    //When the new scene is loaded, the fade in process begins
    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        yield return null;
        faderImg.CrossFadeAlpha(0, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        faderCanvas.SetActive(false);
    }

    IEnumerator FadeOut(string sceneName)
    {
        faderCanvas.SetActive(true);
        faderImg.canvasRenderer.SetAlpha(0);
        faderImg.CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
