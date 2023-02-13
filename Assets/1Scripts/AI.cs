using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] float Speed = 3.0f; // 이동 스피드


    [SerializeField] GameObject Projectile; // 포탄 오브젝트
    [SerializeField] GameObject ShotPos; // 총알 발사 위치

    Vector3 RandPos;
    float ShotCoolDown = 3.0f;  // 샷 쿨타임
    float LastshotTime = 0.0f; // 마지막으로 사격한 시간


    void Start()
    {
        RandPos = new Vector3(Random.Range(0f, 4f), transform.position.y, transform.position.z);
    }

    void Update()
    {
        TryMove();
        TryShot();  

    }

    void TryShot()
    {
        if(Time.time - LastshotTime > ShotCoolDown)
        {
            Instantiate(Projectile, ShotPos.transform.position, ShotPos.transform.rotation);
            LastshotTime = Time.time;
        }
    }

    void TryMove()
    {
        if(Vector3.Distance(transform.position, RandPos) <= 0.1f)
        {
            RandPos = new Vector3(Random.Range(0f, 4f), transform.position.y, transform.position.z);
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, RandPos, Speed * Time.deltaTime);
        }
    }
}
