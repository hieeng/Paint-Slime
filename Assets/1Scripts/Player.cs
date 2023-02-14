using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] float Speed = 3.0f; // 이동 스피드

    [SerializeField] GameObject Projectile; // 포탄 오브젝트
    [SerializeField] GameObject ShotPos; // 총알 발사 위치

    bool IsMove = false;

    float LastMousePos; // 처음 마우스가 눌린 위치
    float XVecter; // 현재 마우스 위치 - 처음 마우스가 눌린 위치

    [SerializeField] float shotCoolDown = 3.0f;  // 샷 쿨타임
    float LastshotTime = 0.0f; // 마지막으로 사격한 시간

    void Start()
    {
    }

    void Update()
    {
        TryMove();
        TryShot();
    }



    void TryShot()
    {
        if (GameManager.Instance.timeOver)
            return;
        if(Time.time - LastshotTime > shotCoolDown)
        {
            GameObject obj = GameManager.Instance.GetObj(0);
            obj.transform.position = ShotPos.transform.position;

            LastshotTime = Time.time;
        }
    }



    void TryMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            IsMove = true;
            LastMousePos = Input.mousePosition.x;
        }

        else if(Input.GetMouseButton(0))
        {
            XVecter = Input.mousePosition.x - LastMousePos;
            LastMousePos = Input.mousePosition.x;

            MovePosition();
        }

        else if(Input.GetMouseButtonUp(0))
        {
            IsMove = false;
            XVecter = 0f;
        }
    }

    void MovePosition()
    {
        transform.position = ClampPos(transform.position);
        transform.position += Vector3.right * XVecter * Speed * Time.deltaTime;
    }

    Vector3 ClampPos(Vector3 pos)
    {
        return new Vector3(Mathf.Clamp(transform.position.x, -4.5f, 4.5f), transform.position.y, transform.position.z);
    }
}