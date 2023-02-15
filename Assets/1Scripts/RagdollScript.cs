using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    [SerializeField] Collider Thiscol;  // Root의 콜라이더
    [SerializeField] Rigidbody ThisRig; // Root의 리지드바디
    [SerializeField] Rigidbody KnockBackBone = null; // 넉백시 날아갈 본 (허리)
    [SerializeField] public float KnockBackPower = 1000.0f; // 넉백 파워
    [SerializeField] Vector3 forceVec; // 넉백 방향

    [SerializeField] GameObject FireParticle;

    Collider[] col = null; // 래그돌의 콜라이더 배열
    Rigidbody[] rig = null; // 래글돌의 리지드바디 배열

    void Awake()
    {
        GetRagdollBits();
        SetRagdoll(false);
    }


    void GetRagdollBits()   // 래그돌의 콜라이더와 리지드바디를 배열에 넣음
    {
        col = transform.Find("Armature").GetComponentsInChildren<Collider>();
        rig = transform.Find("Armature").GetComponentsInChildren<Rigidbody>();
    }

    void SetRagdoll(bool act) // act가 true면 래그돌 활성화, false면 비활성화
    {
        //Anim.enabled = !act;
        ThisRig.isKinematic = act;
        Thiscol.isTrigger = act;


        for(int i = 0, cnt = rig.Length; i < cnt; i++)
        {
            var rigid = rig[i];
            if(null == rigid)
                continue;

            rigid.isKinematic = !act;
        }

        for(int i = 0, cnt = col.Length; i < cnt; i++)
        {
            var collider = col[i];
            if(null == collider)
                continue;

            collider.isTrigger = !act;
        }
    }

    void SetRagdollSink(bool act)
    {
        for(int i = 0, cnt = col.Length; i < cnt; i++)
        {
            var rigid = rig[i];
            if (null == rigid) continue;

            rigid.isKinematic = !act;
        }
        
        for (int i = 0, cnt = col.Length; i < cnt; i++)
        {
            var collider = col[i];
            if (null == collider)
                continue;

            collider.isTrigger = act;
        }
        // Destroy(this.gameObject, 2.0f); // Sink된 뒤 2초뒤 파괴
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(gameObject.name == "RedKing")    KnockBack();
        }
    }

    void KnockBack()
    {
        FireParticle.SetActive(true);
        SetRagdoll(true);
        KnockBackBone.AddForce(forceVec * KnockBackPower);
        SetRagdollSink(true);
    }
}
