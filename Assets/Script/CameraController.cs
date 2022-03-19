using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static readonly Vector3 vectorFromPlayer=new Vector3(0f, 3f, -1.5f);//Player to camera

    public Camera Camera;
    public GameObject Target;
    public float CameraSpeed = 3;

    private Transform CameraTrans;

    void Start()
    {
        this.CameraTrans = Camera.GetComponent<Transform>();
    }

    void Update()
    {
        Transform target_trans = Target.GetComponent<Transform>();
        float dt = Time.deltaTime;
        Vector3 dv = (target_trans.position + vectorFromPlayer - this.CameraTrans.position) * dt * this.CameraSpeed;
        this.CameraTrans.position += dv;
    }

    public void SetTarget(GameObject Target)
    {
        this.Target = Target;
    }
}
