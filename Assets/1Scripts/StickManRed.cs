using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManRed : StickMan
{
    private void Awake() 
    {
        Init();
        point = GameManager.Instance.pointRed;
        layer = LayerMask.GetMask("BlueSlime");
    }

    private void Start() 
    {
        HitAction();
    }

    private void Update() 
    {
        Move();
        GaterPoint();
        Target(layer);
        //ToMoveTarget();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
        {
            Hit(blue);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
            HitAction();

        if (GameManager.Instance.timeOver)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
                Die();
        }
    }
}
