using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 10.0f;
    [SerializeField] GameObject PaintParticle;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
        ChangeParticleColor();
    }

    void Update()
    {
        
    }


    void OnCollisionEnter(Collision other) 
    {
        Play_PaintParticle();
        gameObject.SetActive(false);
    }

    void Play_PaintParticle()
    {
        var paint = Instantiate(PaintParticle, transform.position, transform.rotation);
        PaintParticle.GetComponent<ParticleSystem>().Play();
    }

    void ChangeParticleColor()
    {
        for(int i = 0; i < PaintParticle.GetComponent<Transform>().childCount; i++)
        {
            ParticleSystem.MainModule setting = PaintParticle.GetComponent<Transform>().GetChild(i).GetComponent<ParticleSystem>().main;
            setting.startColor = GetComponent<MeshRenderer>().material.color;
        }
    }
}
