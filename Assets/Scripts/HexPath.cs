using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPath : MonoBehaviour
{
    public enum Color {Neutral, Red, Blue, Yellow}
    public Color pathColor;
    BoxCollider2D _boxCollider2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        //String to Enum. The string being used is this gameobject's tag
        pathColor = (Color)System.Enum.Parse(typeof(Color), this.gameObject.tag);
        
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _boxCollider2D.size = _spriteRenderer.size;

        if(transform.parent != null)
        {
            _boxCollider2D.size = gameObject.GetComponentInParent<BoxCollider2D>().size;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (pathColor)
        {
            case Color.Red:
                _boxCollider2D.enabled = SwitchManager.SharedInstance.redPathActivated;
                _animator.SetBool("Activated", SwitchManager.SharedInstance.redPathActivated);
                break;
            case Color.Blue:
                _boxCollider2D.enabled = SwitchManager.SharedInstance.bluePathActivated;
                _animator.SetBool("Activated", SwitchManager.SharedInstance.bluePathActivated);
                break;
            case Color.Yellow:
                _boxCollider2D.enabled = SwitchManager.SharedInstance.yellowPathActivated;
                _animator.SetBool("Activated", SwitchManager.SharedInstance.yellowPathActivated);
                break;                                
        }
    }
}
