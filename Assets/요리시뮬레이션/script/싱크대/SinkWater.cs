using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkWater : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void On()
    {
        anim.SetInteger("state", 1);
    }
    public void Off()
    {
        anim.SetInteger("state", 2);
    }
}
