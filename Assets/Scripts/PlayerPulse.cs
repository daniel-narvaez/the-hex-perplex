using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPulse : HexSpell
{
    public int dashForce;
    public float _gravityScale;

    public override void Start()
    {
        base.Start();
        _gravityScale = _rigidbody2D.gravityScale;
    }

    // void Update()
    // {
    //     if(_playerMovement.grounded && !_animationManager.currentState.Contains("_Non_Int"))
    //         canDash = true;
    // }

    public override void TriggerCoroutine(InputActionPhase direction)
    {
        if (readyToUse == false)
            return;

        AudioManager.SharedInstance.PlayClip("PulseDash");
        CreateHexSpell(direction);
        StartCoroutine(CastHexSpell(direction));
    }

    public override void CreateSpawnPoint()
    {
        spawnPoint = new GameObject("PulseSpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(0f, -0.125f);
    }

    public override void CreateHexSpell(InputActionPhase direction)
    {
        GameObject pulse = ObjectPooler.SharedInstance.GetPooledObject("Lightning");
        if (pulse != null)
        {
            pulse.transform.position = spawnPoint.transform.position;
            pulse.SetActive(true);
            if (direction == InputActionPhase.Waiting)
            {
                pulse.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                pulse.transform.localScale = new Vector2(1, _playerMovement.orientation);
            }
            else if (direction == InputActionPhase.Started)
            {
                pulse.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                pulse.transform.localScale = new Vector2(_playerMovement.orientation, 1);
            }
        }
    }

    public override IEnumerator CastHexSpell(InputActionPhase direction)
    {
        readyToUse = false;

        if (direction == InputActionPhase.Waiting)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.AddForce(new Vector2(dashForce * _playerMovement.orientation, 75f));
            _animationManager.ChangeAnimationState("Player_InitiateDash_Non_Int");
            yield return new WaitForSeconds(0.1f);
        
            HexSpellManager.SharedInstance.ChangeCurrentSpell(0, " ");
            InterfaceManager.SharedInstance.UpdateInterface(0, " ");
            yield return new WaitForSeconds(0.275f);

            _rigidbody2D.gravityScale = _gravityScale;
            _animationManager.currentState = "null";
        }
        else if (direction == InputActionPhase.Started)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0f, dashForce));
            _animationManager.ChangeAnimationState("Player_InitiateDash_Non_Int");
            yield return new WaitForSeconds(0.1f);

            HexSpellManager.SharedInstance.ChangeCurrentSpell(0, " ");
            InterfaceManager.SharedInstance.UpdateInterface(0, " ");
            yield return new WaitForSeconds(0.275f);

            _animationManager.currentState = "null";
        }

    }
}
