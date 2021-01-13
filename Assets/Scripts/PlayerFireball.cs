using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireball : HexSpell
{
    
    public override void TriggerCoroutine(InputActionPhase direction)
    {
        if(readyToUse == false)
            return;

        AudioManager.SharedInstance.PlayClip("PyroShot");
        CreateHexSpell(inputBinding.phase);
        StartCoroutine(CastHexSpell(inputBinding.phase));       
    }

    public override void CreateSpawnPoint()
    {
        spawnPoint = new GameObject("FireballSpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(0.39f, -0.28125f);
    }

    public override void CreateHexSpell(InputActionPhase direction)
    { 
        GameObject fireball = ObjectPooler.SharedInstance.GetPooledObject("Fire");
        if (fireball != null)
        {
            fireball.transform.position = spawnPoint.transform.position;
            fireball.SetActive(true);

            fireball.transform.parent = this.transform;
            fireball.transform.localScale = new Vector2(1, 1);
        }
    }

    public override IEnumerator CastHexSpell(InputActionPhase direction)
    {
        readyToUse = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animationManager.ChangeAnimationState("Player_LaunchFireball");
        yield return new WaitForSeconds(0.75f);

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.25f);

        HexSpellManager.SharedInstance.ChangeCurrentSpell(0, " ");
        InterfaceManager.SharedInstance.UpdateInterface(0, " ");
    }
}
