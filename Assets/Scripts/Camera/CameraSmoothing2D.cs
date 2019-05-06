using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothing2D : MonoBehaviour
{
    public float dampTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    public float deadzoneRight;
    public float deadzoneLeft;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
            Vector3 destination = transform.position + delta;
            if (destination.x < deadzoneLeft)
            {
                destination.x = deadzoneLeft;
            }
            else if(destination.x > deadzoneRight)
            {
                destination.x = deadzoneRight;
            }
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
