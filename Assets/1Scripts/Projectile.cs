using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    [SerializeField] GameObject PaintParticle;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }


    void OnCollisionEnter(Collision other) 
    {
        Play_PaintParticle();
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void Play_PaintParticle()
    {
        var paint = Instantiate(PaintParticle, transform.position, transform.rotation);
        PaintParticle.GetComponent<ParticleSystem>().Play();
    }
}
