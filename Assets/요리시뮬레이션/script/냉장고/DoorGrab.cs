using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrab : MonoBehaviour
{
    public Transform handle;
    public OVRGrabbable grab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!grab.isGrabbed)
        {
            transform.position = handle.position;
            transform.rotation = handle.rotation;

            Rigidbody handle_rigid_body = handle.GetComponent<Rigidbody>();
            handle_rigid_body.velocity = Vector3.zero;
            handle_rigid_body.angularVelocity = Vector3.zero;
        }
    }
}
