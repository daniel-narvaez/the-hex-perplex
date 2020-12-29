using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGlacier : MonoBehaviour
{
    public InputAction glacialWave;
    GameObject spawnPoint;
    bool canShoot = true;
    public LayerMask groundLayer;

    //Movement
    Rigidbody2D _rigidbody2D;
    PlayerMovement _playerMovement;

    //Animation
    AnimationManager _animationManager;

    void OnEnable()
    {
        glacialWave.Enable();
    }

    void OnDisable()
    {
        glacialWave.Disable();
    }

    void Awake()
    {
        glacialWave.started += ctx => TriggerCoroutine();
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();

        _animationManager = GetComponent<AnimationManager>();

        CreateSpawnPoint();
    }

    void TriggerCoroutine()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(spawnPoint.transform.position, Vector2.down, 0.1f, groundLayer);
        if(_playerMovement.grounded == false || canShoot == false || hit2D.collider == null)
            return;
        
        AudioManager.SharedInstance.PlayClip("GlacialWave");
        CreateGlacier(hit2D);
        StartCoroutine(ShootGlacier());
    }

    void CreateSpawnPoint()
    {
        spawnPoint = new GameObject("GlacierSpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(0.5625f, -0.6095f);
    }

    void CreateGlacier(RaycastHit2D hit2D)
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

    IEnumerator ShootGlacier()
    {
        canShoot = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animationManager.ChangeAnimationState("Player_ShootGlacier");
        yield return new WaitForSeconds(0.5f);

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.75f);

        canShoot = true;
    }
}
