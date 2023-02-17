//미치 : 카메라 애니메이션 컨트롤하는 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    static readonly int ANIM_Trigger = Animator.StringToHash("CameraTrigger");

    [SerializeField] public Animator Anim;

}
