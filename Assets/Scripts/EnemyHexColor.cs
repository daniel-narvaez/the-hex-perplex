using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHexColor : MonoBehaviour
{
    public enum Color {Neutral, RedEnemy, BlueEnemy, YellowEnemy}
    public Color enemyColor;
    // Start is called before the first frame update
    void Start()
    {
        //String to Enum. The string being used is this gameobject's tag
        enemyColor = (Color)System.Enum.Parse(typeof(Color), this.gameObject.tag);
    }
}
