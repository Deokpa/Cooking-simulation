using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandle : MonoBehaviour
{
    public Transform grabable_handle;

    Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        body.MovePosition(grabable_handle.position);
    }

}
