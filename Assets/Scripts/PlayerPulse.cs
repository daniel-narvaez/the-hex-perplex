using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPulse : MonoBehaviour
{
    public InputAction pulseDash, Up;
    GameObject spawnPoint;
    bool canDash = true;
    public int dashForce;
    float _gravityScale;

    //Movement
    Rigidbody2D _rigidbody2D;
    PlayerMovement _playerMovement;

    //Animation
    AnimationManager _animationManager;

    void OnEnable()
    {
        pulseDash.Enable();
        Up.Enable();
    }

    void OnDisable()
    {
        pulseDash.Disable();
        Up.Disable();
    }

    void Awake()
    {
        pulseDash.started += ctx => TriggerCoroutine(Up.phase);
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gravityScale = _rigidbody2D.gravityScale;
        _playerMovement = GetComponent<PlayerMovement>();

        _animationManager = GetComponent<AnimationManager>();

        CreateSpawnPoint();
    }

    void Update()
    {
        if(_playerMovement.grounded && !_animationManager.currentState.Contains("_Non_Int"))
            canDash = true;
    }

    void TriggerCoroutine(InputActionPhase direction)
    {
        if (canDash == false)
            return;

        AudioManager.SharedInstance.PlayClip("PulseDash");
        CreatePulse(direction);
        StartCoroutine(InitiateDash(direction));
    }

    void CreateSpawnPoint()
    {
        spawnPoint = new GameObject("PulseSpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(0f, -0.125f);
    }

    void CreatePulse(InputActionPhase direction)
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

    IEnumerator InitiateDash(InputActionPhase direction)
    {
        canDash = false;

        if (direction == InputActionPhase.Waiting)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.AddForce(new Vector2(dashForce * _playerMovement.orientation, 75f));
            _animationManager.ChangeAnimationState("Player_InitiateDash_Non_Int");
            yield return new WaitForSeconds(0.375f);
            
            _rigidbody2D.gravityScale = _gravityScale;
            _animationManager.currentState = "null";
        }
        else if (direction == InputActionPhase.Started)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0f, dashForce));
            _animationManager.ChangeAnimationState("Player_InitiateDash_Non_Int");
            yield return new WaitForSeconds(0.375f);

            _animationManager.currentState = "null";
        }
    }
}
