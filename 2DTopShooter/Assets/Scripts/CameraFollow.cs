using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float SmoothFactor1 = 0.3f;
    public float SmoothFactor2 = 0.3f;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        Target.GetComponent<PlayerController>().OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(object sender, DeathEventArgs e)
    {
        Target = transform;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, mousePos.x, Time.deltaTime * SmoothFactor1),
            Mathf.Lerp(transform.position.y, mousePos.y, Time.deltaTime * SmoothFactor1),
            transform.position.z);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, Target.position.x, Time.fixedDeltaTime * SmoothFactor1),
            Mathf.Lerp(transform.position.y, Target.position.y, Time.fixedDeltaTime * SmoothFactor2),
            transform.position.z);
    }
}
