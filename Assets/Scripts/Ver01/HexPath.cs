using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPath : MonoBehaviour
{
    public enum Color {Red, Blue, Yellow}
    public Color pathColor;
    BoxCollider2D _boxCollider2D;
    Animator _animator;
    void Start()
    {
        //String to Enum. The string being used is this gameobject's tag
        pathColor = (Color)System.Enum.Parse(typeof(Color), this.gameObject.tag);
        
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        switch (pathColor)
        {
            case Color.Red:
                _boxCollider2D.enabled = SwitchManager.SharedInstance.redPathActivated;
                if(SwitchManager.SharedInstance.redPathActivated)
                    _animator.Play("HexPath_Active");
                else
                    _animator.Play("HexPath_Inactive");
                break;
            case Color.Blue:
                _boxCollider2D.enabled = SwitchManager.SharedInstance.bluePathActivated;
                if(SwitchManager.SharedInstance.bluePathActivated)
                    _animator.Play("HexPath_Active");
                else
                    _animator.Play("HexPath_Inactive");
                break;
            case Color.Yellow:
                _boxCollider2D.enabled = SwitchManager.SharedInstance.yellowPathActivated;
                if(SwitchManager.SharedInstance.yellowPathActivated)
                    _animator.Play("HexPath_Active");
                else
                    _animator.Play("HexPath_Inactive");
                break;                                
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
