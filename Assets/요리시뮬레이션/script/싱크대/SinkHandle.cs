using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkHandle : MonoBehaviour
{
    OVRGrabbable grab;
    Vector3 origin;
    Vector3 handle_origin;
    Quaternion grab_origin_quat;

    public bool test = false;

    public Animator sink_anim;
    public Transform handle_pivot;
    public Transform handle;
    public float weight = 100.0f;
    void Start()
    {
        grab = GetComponent<OVRGrabbable>();
        origin = transform.position;
        handle_origin = handle.transform.position;

        grab_origin_quat = handle_pivot.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (grab == null)
            return;
        if(!grab.isGrabbed)
        {
            transform.position = handle.position;
            transform.rotation = handle.rotation;
            grab_origin_quat = handle_pivot.transform.rotation;
            origin = transform.position;
        }
        else
        {
            float dx = (transform.position.x - origin.x) * weight;
            if (dx > 1)
                dx = 1;
            if (dx < -1)
                dx = 1;
            handle_pivot.rotation = grab_origin_quat * Quaternion.Euler(0, -dx * 80.0f, 0);
            sink_anim.SetFloat("value", (handle.position.x - handle_origin.x) * 100.0f);
        }
    }
}
