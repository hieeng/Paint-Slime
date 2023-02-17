using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProject : Projectile
{
    private void Awake() 
    {
        Init();
        ObjNum = Random.Range(2, 5);
    }

    private void Update() 
    {
        Move();
    }

    private void OnCollisionEnter(Collision other) 
    {
        Play_PaintParticle();
        if (other.gameObject.layer == LayerMask.NameToLayer("RedSlime")
            || other.gameObject.layer == LayerMask.NameToLayer("WhiteSlime"))
        {
            if (!GameManager.Instance.GetIsFever())
            {
                GameManager.Instance.PlusFeverGage();
            }
        }
    }
}
