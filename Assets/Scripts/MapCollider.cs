using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollider : MonoBehaviour
{
    BoxCollider2D _boxCollider2D;
    SpriteRenderer _spriteRenderer;
    public bool special;
    public float xOffset, yOffset;

    void OnEnable()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (special == false)
            _boxCollider2D.size = _spriteRenderer.size;
        else if (special == true)
        {
            _boxCollider2D.offset = Vector2.zero;
            _boxCollider2D.size = new Vector2(_spriteRenderer.size.x + xOffset, _spriteRenderer.size.y + yOffset);
        }
    }
}
