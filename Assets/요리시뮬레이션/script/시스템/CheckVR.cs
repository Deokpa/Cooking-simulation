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
            pc_controller.LockControl = false;
        }
        else
        {
            pc_controller.LockControl = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
