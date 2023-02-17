using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProject : Projectile
{
    private void Awake() 
    {
        Init();
        ObjNum = Random.Range(5, 8);
    }

    private void Update() 
    {
        Move();
    }

    private void OnCollisionEnter(Collision other) 
    {
        Play_PaintParticle();
    }
}
