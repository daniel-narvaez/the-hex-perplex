using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexSpellManager : Manager
{
    public static HexSpellManager SharedInstance;
    public GameObject player;
    public HexSpell newSpell;
    public List<HexSpell> _hexSpells;

    public int storedIndex;
    public string storedName;

    void Awake()
    {
        if (FindObjectsOfType<HexSpellManager>().Length > 1)
            Destroy(gameObject);

        if(SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Reset();
    }

    public override void Reset()
    {
        _hexSpells.Clear();
        player = GameObject.FindGameObjectWithTag("Player");
        foreach(HexSpell spell in player.GetComponents<HexSpell>())
        {
            _hexSpells.Add(spell);
        }
        storedIndex = 0;
        storedName = " ";
    }

    public void ChangeCurrentSpell(int i, string name)
    {
        foreach (HexSpell spell in _hexSpells)
        {
            spell.readyToUse = false;
        }
        storedIndex = i;
        storedName = name;
        newSpell = _hexSpells.Find(spell => spell.Name == name);
        if (newSpell == null)
            return;
        else
        {
            newSpell.readyToUse = true;
        }
        
    }

}
