using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    BoxCollider2D _boxCollider2D;
    AnimationManager _animationManager;
    ParticleSystem _particleSystem;
    public enum Spell{Fire, Ice, Lightning}
    public Spell availableSpell;

    public int spellIndex;
    public string spellName;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animationManager = GetComponent<AnimationManager>();
        _particleSystem = GetComponent<ParticleSystem>();

        //String to Enum. The string being used is this gameobject's tag
        availableSpell = (Spell)System.Enum.Parse(typeof(Spell), this.gameObject.tag);

        switch (availableSpell)
        {
            case Spell.Fire:
                spellIndex = 1;
                spellName = "Pyro Shot";
                break;
            case Spell.Ice:
                spellIndex = 2;
                spellName = "Glacial Wave";
                break;
            case Spell.Lightning:
                spellIndex = 3;
                spellName = "Pulse Dash";
                break;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (spellIndex == HexSpellManager.SharedInstance.storedIndex || spellName == HexSpellManager.SharedInstance.storedName)
            return;
            
        StartCoroutine(SuspendItem());
    }

    IEnumerator SuspendItem()
    {
        HexSpellManager.SharedInstance.ChangeCurrentSpell(spellIndex, spellName);
        InterfaceManager.SharedInstance.UpdateInterface(spellIndex, spellName);
        _particleSystem.Play();
        AudioManager.SharedInstance.PlayClip("SpellBook");
        _animationManager.ChangeAnimationState("SpellBook_Inactive");
        _boxCollider2D.enabled = false;
        yield return new WaitForSeconds(2.66f);

        _animationManager.ChangeAnimationState("SpellBook_Respawning");
        yield return new WaitForSeconds(0.33f);

        _boxCollider2D.enabled = true;
    }
}
