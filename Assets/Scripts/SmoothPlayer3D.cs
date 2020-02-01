using UnityEngine;
using System.Collections;

public class SmoothPlayer3D : MonoBehaviour
{
    public Transform target;
    public float upDistance;
    public float backDistance;
    public float trackingSpeed;
    public float rotationSpeed;

    private Vector3 v3To;
    private Quaternion qTo;

    void LateUpdate()
    {
        v3To = target.position - target.forward * backDistance + target.up * upDistance;
        transform.position = Vector3.Lerp(transform.position, v3To, trackingSpeed * Time.deltaTime);
        qTo = Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
    }
}

