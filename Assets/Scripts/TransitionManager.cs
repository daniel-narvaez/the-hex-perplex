using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : Manager
{
    public static TransitionManager SharedInstance;
    public GameObject faderCanvas;
    public Image faderImage;
    public float fadeTime;
    
    private void Awake()
    {        
        if (FindObjectsOfType<TransitionManager>().Length > 1)
            Destroy(gameObject);

        if(SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        faderCanvas.SetActive(true);
        faderImage.canvasRenderer.SetAlpha(1);
    }  

    //Called by other scripts. Begins the fade out process
    public void NextScene(Color color, string sceneName, float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(color, sceneName, newFadeTime));
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(Color.black, SceneManager.GetActiveScene().path, 0.5f));
    }

    //When the new scene is loaded, the fade in process begins
    public override void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        yield return null;
        faderImage.CrossFadeAlpha(0, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        faderCanvas.SetActive(false);
    }

    IEnumerator FadeOut(Color color, string sceneName, float newFadeTime)
    {
        fadeTime = newFadeTime;
        faderCanvas.SetActive(true);
        faderImage.color = color;
        faderImage.canvasRenderer.SetAlpha(0);
        faderImage.CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
