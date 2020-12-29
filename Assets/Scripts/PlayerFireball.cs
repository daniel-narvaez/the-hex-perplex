using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireball : MonoBehaviour
{
    public InputAction pyroShot;
    GameObject spawnPoint;
    bool canLaunch = true;

    //Movement
    Rigidbody2D _rigidbody2D;
    PlayerMovement _playerMovement;

    //Animation
    AnimationManager _animationManager;
    
    void OnEnable()
    {
        pyroShot.Enable();
    }

    void OnDisable()
    {
        pyroShot.Disable();
    }

    void Awake()
    {
        pyroShot.started += ctx => TriggerCoroutine();
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
        if(canLaunch == false)
            return;

        AudioManager.SharedInstance.PlayClip("PyroShot");
        CreateFireball();
        StartCoroutine(LaunchFireball());       
    }

    void CreateSpawnPoint()
    {
        spawnPoint = new GameObject("FireballSpawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.localPosition = new Vector2(0.39f, -0.28125f);
    }

    void CreateFireball()
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

    IEnumerator LaunchFireball()
    {
        canLaunch = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animationManager.ChangeAnimationState("Player_LaunchFireball");
        yield return new WaitForSeconds(0.75f);

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;        
        yield return new WaitForSeconds(0.25f);

        canLaunch = true;
    }
}
