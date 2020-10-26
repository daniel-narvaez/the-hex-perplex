using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    BoxCollider2D _boxCollider2D;
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _boxCollider2D.size = _spriteRenderer.size;        
    }
}
