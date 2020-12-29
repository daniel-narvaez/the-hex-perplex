using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction Move, Jump;
    Rigidbody2D _rigidbody2D;

    //Directional Movement
    float move;
    public float moveSpeed;

    //Jumping
    public bool grounded;
    public Transform contactPoint;
    public float contactRadius;
    public LayerMask groundLayer;
    public float jumpForce;
    float jumpDurationCounter;
    public float maxJumpDuration;
    public bool jumping;

    //Animation
    AnimationManager _animationManager;
    public float orientation;

    //Miscellaneous
    RaycastHit2D groundParent;

    void OnEnable()
    {
        Move.Enable();
        Jump.Enable();
    }

    void OnDisable()
    {
        Move.Disable();
        Jump.Disable();
    }

    void Start()
    {
        _animationManager = GetComponent<AnimationManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Directional Movement
        move = Move.ReadValue<Vector2>().x;
        orientation = transform.localScale.x;

        if(_rigidbody2D.bodyType == RigidbodyType2D.Dynamic && !_animationManager.currentState.Contains("_Non_Int"))
        {
            if (Mathf.Abs(move) <= 0.25f) //Idle
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                if(grounded)
                    _animationManager.ChangeAnimationState("Player_Idle");
            }
            else if (Mathf.Abs(move) > 0.25f && Mathf.Abs(move) <= 0.7f) //TipToeing
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Round(move * 2f) * 0.35f * moveSpeed, _rigidbody2D.velocity.y);
                if(grounded)
                    _animationManager.ChangeAnimationState("Player_TipToe");
            }
            else if (Mathf.Abs(move) > 0.7f) //Running
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Round(move) * moveSpeed, _rigidbody2D.velocity.y);
                if(grounded)
                    _animationManager.ChangeAnimationState("Player_TipToe");
            }

            if((move < -0.25f && orientation > 0) || (move > 0.25f && orientation < 0))
            {
                transform.localScale *= new Vector2(-1, 1);
            } 
        }
        #endregion

        #region Jumping
        grounded = Physics2D.OverlapCircle(contactPoint.position, contactRadius, groundLayer);

        if(_rigidbody2D.bodyType == RigidbodyType2D.Dynamic && !_animationManager.currentState.Contains("_Non_Int"))
        {
            if (grounded && _rigidbody2D.velocity.y == 0 && Jump.phase == InputActionPhase.Started) //Pressed Jump
            {              
                jumping = true;
                jumpDurationCounter = maxJumpDuration;

                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);

                AudioManager.SharedInstance.PlayClip("Jump");
            }
            if (Jump.phase == InputActionPhase.Performed && jumping) //Holding Jump
            {
                if(jumpDurationCounter > 0)
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
                    jumpDurationCounter -= Time.deltaTime;
                } else {
                    jumping = false;
                }
            }
            if (Jump.phase == InputActionPhase.Waiting)
            {
                jumping = false;
            }
            if(grounded == false)
                _animationManager.ChangeAnimationState("Player_Jumping");
        }
        #endregion
        
        #region Miscellaneous
        groundParent = Physics2D.Raycast(contactPoint.position, Vector2.down, contactRadius, groundLayer);
        if(groundParent.collider != null)
            transform.parent = groundParent.collider.transform;
        else
            transform.parent = null;
        #endregion
    }
}
