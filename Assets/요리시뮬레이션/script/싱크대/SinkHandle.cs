using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkHandle : MonoBehaviour
{
    OVRGrabbable grab;
    Vector3 origin;
    Quaternion grab_origin_quat;
    float before_dx = 0.0f;
    float dx = 0.0f;

    public bool test = false;

    public Animator sink_anim;
    public SinkWater water;
    public Transform handle_pivot;
    public Transform handle;
    public float weight = 100.0f;
    void Start()
    {
        grab = GetComponent<OVRGrabbable>();
        origin = transform.position;

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
            before_dx = sink_anim.GetFloat("value");
        }
        else
        {
            dx = (transform.position.x - origin.x) * weight;
            dx = (dx > 1) ? 1 : (dx < -1) ? -1 : dx;
            if (dx + before_dx < 0)
                dx = -before_dx;
            if (dx + before_dx > 1)
                dx = 1 - before_dx;
            handle_pivot.rotation = grab_origin_quat * Quaternion.Euler(0, -dx * 50.0f, 0);
            sink_anim.SetFloat("value", before_dx + dx);
            if (water != null)
            {
                if (before_dx + dx > 0.8)
                    water.On();
                else if (before_dx + dx < 0.2)
                    water.Off();
            }
        }
    }
}
