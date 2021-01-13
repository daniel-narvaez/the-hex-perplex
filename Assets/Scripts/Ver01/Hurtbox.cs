using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hurtbox : MonoBehaviour
{
    public GameObject parent;
    public AnimationManager _animationManager;
    public Rigidbody2D _rigidbody2D;
    public virtual void Start()
    {
        parent = this.transform.parent.gameObject;
        _animationManager = parent.GetComponent<AnimationManager>();
        _rigidbody2D = parent.GetComponent<Rigidbody2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Death());
    }

    public abstract IEnumerator Death();
}
