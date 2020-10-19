using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSwitch : MonoBehaviour
{
    public enum Color {Red, Blue, Yellow}
    public Color switchColor;
    Animator _animator;
    void Start()
    {   
        //String to Enum. The string being used is this gameobject's tag
        switchColor = (Color)System.Enum.Parse(typeof(Color), this.gameObject.tag);

        _animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (switchColor)
        {
            case Color.Red:
            if(other.gameObject.tag == ("FireBall"))
            {
                _animator.SetTrigger("Flash");
                SwitchManager.SharedInstance.redPathActivated = !SwitchManager.SharedInstance.redPathActivated;
            }
                break;
            case Color.Blue:
            if(other.gameObject.tag == ("Icicle"))
            {                
                _animator.SetTrigger("Flash");
                SwitchManager.SharedInstance.bluePathActivated = !SwitchManager.SharedInstance.bluePathActivated;
            }
                break;
            case Color.Yellow:
            if(other.gameObject.tag == ("Lightning"))
            {               
                _animator.SetTrigger("Flash");
                SwitchManager.SharedInstance.yellowPathActivated = !SwitchManager.SharedInstance.yellowPathActivated;
            }
                break;                                
        }
    }
}
