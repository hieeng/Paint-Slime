using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManWhite : StickMan
{
    private void Awake() 
    {
        Init();
    }

    private void Update() 
    {
        Move();
        if (Input.GetKeyDown("space"))
        {
            
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
            Hit(blue);
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
            Hit(red);
    }
}
