using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class HexSpell : MonoBehaviour
{
    public string Name;
    public InputAction inputAction, inputBinding;
    public GameObject spawnPoint;
    public bool readyToUse;

    public Rigidbody2D _rigidbody2D;
    public PlayerMovement _playerMovement;
    public AnimationManager _animationManager;

    public virtual void OnEnable()
    {
        inputAction.Enable();
        inputBinding.Enable();
    }

    public virtual void OnDisable()
    {
        inputAction.Disable();
        inputBinding.Disable();
    }

    public virtual void Awake()
    {
        inputAction.started += ctx => TriggerCoroutine(inputBinding.phase);
    }

    public virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();

        _animationManager = GetComponent<AnimationManager>();

        if(spawnPoint == null)
            CreateSpawnPoint();
    }

    public abstract void TriggerCoroutine(InputActionPhase direction);
    public abstract void CreateSpawnPoint();
    public abstract void CreateHexSpell(InputActionPhase direction);
    public abstract IEnumerator CastHexSpell(InputActionPhase direction);
}
