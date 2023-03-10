using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] float Speed = 3.0f; // 이동 스피드


    [SerializeField] GameObject Projectile; // 포탄 오브젝트
    [SerializeField] GameObject ShotPos; // 총알 발사 위치

    Vector3 RandPos;
    [SerializeField] float ShotCoolDown = 1.0f;  // 샷 쿨타임
    float LastshotTime = 0.0f; // 마지막으로 사격한 시간

    Vector3 origin;

    private void Awake() 
    {
        origin = transform.position;
    }

    void Start()
    {
        RandPos = new Vector3(Random.Range(-4.5f, 4.5f), transform.position.y, transform.position.z);
    }

    void Update()
    {
        TryMove();
        TryShot();  
        OriginPos();
    }

    void TryShot()
    {
        if (!GameManager.Instance.gameStart)
            return;
        if (GameManager.Instance.timeOver)
            return;
        if(Time.time - LastshotTime > ShotCoolDown)
        {
            GameObject obj = GameManager.Instance.GetObj(1);
            obj.transform.position = ShotPos.transform.position;

            LastshotTime = Time.time;
        }
    }

    void TryMove()
    {
        if (!GameManager.Instance.gameStart)
            return;
        if (GameManager.Instance.timeOver)
            return;

        if(Vector3.Distance(transform.position, RandPos) <= 0.1f)
        {
            RandPos = new Vector3(Random.Range(-4.5f, 4.5f), transform.position.y, transform.position.z);
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, RandPos, Speed * Time.deltaTime);
        }
    }

    void OriginPos()
    {
        if (!GameManager.Instance.timeOver)
            return;
        transform.position = Vector3.Lerp(transform.position, origin, Speed * Time.deltaTime);
    }
}
