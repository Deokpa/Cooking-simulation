using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWater : MonoBehaviour
{
    public string water_tag;
    public Pot pot;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(water_tag))
        {
            pot.Fill();
        }
    }
}
