using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceManager : Manager
{
    public static InterfaceManager SharedInstance;
    public Image currentSpellIcon;
    public Text currentSpellName;
    public Sprite[] spellIcons; //0 = null, 1 = Pyro Shot, 2 = Glacial Wave, 3 = Pulse Dash

    void Awake()
    {
        if (FindObjectsOfType<InterfaceManager>().Length > 1)
            Destroy(gameObject);

        if(SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public override void Reset()
    {
        if(!SceneManager.GetActiveScene().name.Contains("Level"))
            Destroy(gameObject);
            
        UpdateInterface(0, " ");
    }

    public void UpdateInterface(int i, string name)
    {
        currentSpellIcon.sprite = spellIcons[i];
        currentSpellName.text = name;
    }
}
