using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public OVRGrabbable handle;
    public Transform pos;

    Vector3 original_position;
    Quaternion original_q;
    Vector3 original_v;

    void Start()
    {
        original_position = transform.position;
        original_q = transform.rotation;
        original_v = (handle.transform.position - transform.position).normalized;
    }

    void Update()
    {
        if (handle.isGrabbed)
        {
            Vector3 v = (handle.transform.position - original_position).normalized;
            if (v.z < 0)
            {
                float d = Vector3.Dot(original_v, v);
                float dir = Mathf.Rad2Deg * Mathf.Acos(d);
                Quaternion q = Quaternion.Euler(0, -dir, 0);
                transform.rotation = original_q * q;
            }
        }
    }

}
