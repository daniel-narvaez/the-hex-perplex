using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator _animator;
    public string currentState;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        //Stop the same animation from overriding itself
        if(currentState == newState || currentState.Contains("_Non_Int"))
            return;
        
        //Play the animation
        _animator.Play(newState);

        //Reassign the current state to the new state
        currentState = newState;
    }
}
