using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;

    public Vector3 offsetChangeMultiplier; 
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Player.onCubeCountChange += ChangeOffset;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    }

    private void ChangeOffset(int i)
    {
        Offset.x += i * offsetChangeMultiplier.x;
        Offset.y += i * offsetChangeMultiplier.y;
        Offset.z += i * offsetChangeMultiplier.z;
    }
}
