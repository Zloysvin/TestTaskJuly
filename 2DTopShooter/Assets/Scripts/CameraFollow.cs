using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float SmoothFactor = 0.3f;

    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, Target.position.x, Time.deltaTime * SmoothFactor),
            Mathf.Lerp(transform.position.y, Target.position.y, Time.deltaTime * SmoothFactor),
            transform.position.z);
    }
}
