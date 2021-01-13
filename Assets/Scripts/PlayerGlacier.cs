using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGlacier : HexSpell
{
    public LayerMask groundLayer;
    RaycastHit2D hit2D;

    public override void TriggerCoroutine(InputActionPhase direction)
    {
        hit2D = Physics2D.Raycast(spawnPoint.transform.position, Vector2.down, 0.1f, groundLayer);
        if(_playerMovement.grounded == false || readyToUse == false || hit2D.collider == null)
            return;
        
        AudioManager.SharedInstance.PlayClip("GlacialWave");
        CreateHexSpell(inputBinding.phase);
        StartCoroutine(CastHexSpell(inputBinding.phase));
    }

    public override void CreateSpawnPoint()
    {
        spawnPoint = new GameObject("GlacierSpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(0.5625f, -0.6095f);
    }

    public override void CreateHexSpell(InputActionPhase direction)
    {
        GameObject glacier = ObjectPooler.SharedInstance.GetPooledObject("Ice");
        if (glacier != null)
        {
            glacier.transform.position = spawnPoint.transform.position;
            glacier.SetActive(true);

            glacier.transform.parent = hit2D.collider.transform;
            glacier.transform.localScale = transform.localScale;
        }
    }

    public override IEnumerator CastHexSpell(InputActionPhase direction)
    {
        readyToUse = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animationManager.ChangeAnimationState("Player_ShootGlacier");
        yield return new WaitForSeconds(0.5f);

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.75f);

        HexSpellManager.SharedInstance.ChangeCurrentSpell(0, " ");
        InterfaceManager.SharedInstance.UpdateInterface(0, " ");
    }
}
