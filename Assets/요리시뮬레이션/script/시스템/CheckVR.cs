using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVR : MonoBehaviour
{
    public Controller pc_controller;
    public OVRPlayerController vr_controller;
    bool is_headset_connected = false;

     
    void Start()
    {
        is_headset_connected = OVRManager.isHmdPresent;
        if(is_headset_connected)
        {
            vr_controller.enabled = true;
            pc_controller.enabled = false;
        }
        else
        {
            vr_controller.enabled = false;
            pc_controller.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
