using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public enum State {Idle, Alert}
    public State currentState;
    public AnimationManager _animationManager;
    public Rigidbody2D _rigidbody2D;

    public virtual void Start()
    {
        _animationManager = GetComponent<AnimationManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        CreatePoint();
    }

    public abstract void CreatePoint();

    public virtual IEnumerator InitiateAttack()
    {
        throw new System.NotImplementedException();        
    }

    public virtual IEnumerator RepeatAttack()
    {
        throw new System.NotImplementedException(); 
    }
}
