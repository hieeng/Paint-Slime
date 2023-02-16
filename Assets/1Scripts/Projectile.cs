using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    [SerializeField] GameObject PaintParticle;

    [SerializeField] int ObjNum;

    float MadeTime;
    void Awake()
    {
        MadeTime = Time.time;
    }

    void Update()
    {
        if(Time.time - MadeTime > 5.0f) Destroy(gameObject);

        if(GameManager.Instance.timeOver == true) Destroy(gameObject);
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
        var paint = GameManager.Instance.GetObj(ObjNum);
        paint.transform.position = transform.position;
        paint.transform.rotation = transform.rotation;
        paint.GetComponent<ParticleSystem>().Play();
        // var paint = Instantiate(PaintParticle, transform.position, transform.rotation);
        // PaintParticle.GetComponent<ParticleSystem>().Play();
    }
}
