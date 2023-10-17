using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public float water_amount = 0.0f;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill()
    {
        if(water_amount < 1.0f)
        {
            water_amount += 0.1f * Time.deltaTime;
            anim.SetFloat("water", water_amount);
        }
    }
}
