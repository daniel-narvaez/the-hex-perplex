using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   

    //For this project, I think it's best if we use the new input system, so that we can link
    //player controls to code much easier. The new input system creates a namespace (InputSystem).
    //I've gone ahead and created controls for movement.

    //This right here is a reference to the Input Action Asset "PlayerControls"
    PlayerControls controls;

    //Animation
    Animator _animator;
    public static float orientation;

    //Basic Movement
    Vector2 move; 
    public float speed = 4;
    Rigidbody2D _rigidbody;
    private int dash;
    public int dashValue;
    public float dashSpeed;
    private float dashTimer;
    public float dashDuration;

    public float _gravity;

    //Jumping
    public bool grounded;
    public Transform contactPoint;
    public float contactRadius;
    public LayerMask groundLayer;
    public float jumpForce = 400;
    private int doubleJump;
    public int doubleJumpValue;

    //Attacks
    public Transform fireBallPoint;
    public Transform iciclePoint;
    public Transform lightningPoint;
    GameObject lightning;

    //Hitboxes/Hurtboxes
    BoxCollider2D hurtbox;
    public float knockbackForce;
    public bool damaged;
    public ScreenBoundaries screenBoundaries;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Jump.performed += ctx => IcicleJump();
        controls.Gameplay.Shoot.performed += ctx => FireBallShot();
        controls.Gameplay.Dash.performed += ctx => LightningDash();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        //Sets the playing in position of the level's starting point
        transform.position = Checkpoint.CurrentlyActiveCheckpoint();
        screenBoundaries = GetComponent<ScreenBoundaries>();
        orientation = transform.localScale.x;

        //lightning dash is always wherever the player is
        lightning = ObjectPooler.SharedInstance.GetPooledObject("Lightning");
        lightning.transform.parent = this.transform;
        lightning.transform.position = lightningPoint.position;

        dash = dashValue;
        dashTimer = dashDuration;
        doubleJump = doubleJumpValue;
        _rigidbody.gravityScale = _gravity;

        //the player knows about its own hitbox/hurtboxes
        hurtbox = transform.Find("Hurtbox").GetComponent<BoxCollider2D>();
        damaged = false;
    }
    void Update()
    { 
        //Code for checking if player is grounded
        grounded = Physics2D.OverlapCircle(contactPoint.position, contactRadius, groundLayer);
        //Code for moving left and right
        Vector2 movement = new Vector2(move.x * speed, _rigidbody.velocity.y);

        orientation = transform.localScale.x;
        _animator.SetFloat("MovementSpeed", Mathf.Abs(movement.x));
        _animator.SetFloat("VerticalVelocity", movement.y);
        _animator.SetBool("Grounded", grounded);

        //flips the player's sprite to match the direction they are facing
        if((movement.x < 0 && orientation > 0) || (movement.x > 0 && orientation < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        } 

        //checks to see if the player is on the ground or not. Resets Jump Values/Dash Timer.
        if(grounded)
        {    
            doubleJump = doubleJumpValue;
            if(dashTimer <= 0)
            {
                dash = dashValue;
                dashTimer = dashDuration;
            }
        }

        if(damaged == false)
        {
            //changes the player's method of velocity based on whether they are dashing.
            if(dash > 0 || dashTimer <= 0)
            {
                lightning.SetActive(false);
                controls.Gameplay.Jump.Enable();
                _rigidbody.gravityScale = _gravity;
                _rigidbody.velocity = movement;
                hurtbox.enabled = true;
            } 
            else if(dash == 0) 
            {
                dashTimer -= Time.deltaTime;
                _rigidbody.gravityScale = 0;
                hurtbox.enabled = false;
            }
        }
    }

    //Pressing Button South(Gamepad)/Space(Keyboard) will trigger the jump
    void Jump()
    {
        if(grounded && _rigidbody.velocity.y == 0)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    //Pressing Button West(Gamepad)/K(Keyboard) will trigger the fireball shot
    void FireBallShot()
    {
        GameObject fireBall = ObjectPooler.SharedInstance.GetPooledObject("FireBall");
        if (fireBall != null)
        {
            _animator.SetTrigger("Shooting");
            fireBall.transform.position = fireBallPoint.position;
            fireBall.SetActive(true);
        }
    }

    //Pressing Button South(Gamepad)/Space(Keyboard) WHILE IN MIDAIR will trigger the icicle jump
    void IcicleJump()
    {
        if(doubleJump > 0 && grounded == false)
        {
            doubleJump--;
            _animator.SetTrigger("IcicleJumping");
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(0, jumpForce * 0.85f));

            GameObject icicle = ObjectPooler.SharedInstance.GetPooledObject("Icicle");
            if (icicle != null) 
            {
                icicle.transform.position = iciclePoint.position;
                icicle.SetActive(true);
            }
        }
    }

    //Pressing Right Shoulder(Gamepad)/L(Keyboard) will trigger the Lightning Dash
    void LightningDash()
    {
        if(dash > 0)
        {
            dash--;
            _animator.SetTrigger("Dashing");
            if (lightning != null) 
            {
                lightning.SetActive(true);
            }
            controls.Gameplay.Jump.Disable();            
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(dashSpeed * orientation, 0));
        }
    }

    public IEnumerator HitByAttack()
    {
        _animator.SetTrigger("Death");
        damaged = true;
        controls.Gameplay.Disable(); //disabled due to death
        hurtbox.enabled = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        //_rigidbody.AddForce(new Vector2(knockbackForce * (orientation * -1), 0));

        yield return new WaitForSeconds(0.33f); //damaged animation duration
        _rigidbody.velocity = Vector2.zero;
        screenBoundaries.enabled = false; //allows the player to move their position to a checkpoint beyond boundaries

        yield return new WaitForSeconds(1.0f); //death animation duration
        transform.position = Checkpoint.CurrentlyActiveCheckpoint(); //moves player to last checkpoint
        _rigidbody.gravityScale = _gravity;
        damaged = false;
        controls.Gameplay.Enable(); //re-enabled due to respawn

        yield return new WaitForSeconds(0.5f);
        hurtbox.enabled = true;
        screenBoundaries.enabled = true;
    }
}
