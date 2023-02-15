using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    static readonly int ANIM_Trigger = Animator.StringToHash("CameraTrigger");

    [SerializeField] public Animator Anim;

}
