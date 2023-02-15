using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScripts : MonoBehaviour
{
    [SerializeField] GameObject FireWork;
    [SerializeField] ParticleSystem[] FireWorkList;
    [SerializeField] GameObject WaterParticle;

    void Start() 
    {
        FireWorkList = FireWork.GetComponentsInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other) 
    {
        var waterparticle = Instantiate(WaterParticle, other.transform.position, Quaternion.identity);
        waterparticle.GetComponent<ParticleSystem>().Play();

        FireWork.SetActive(true);
        foreach(var list in  FireWorkList)
        {
            list.Simulate(3.5f, false, true, true);
            list.Play();
        }
    }
}
