using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float torque;
    public GameObject planet;

    Rigidbody rb;
    float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        OrientBody();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 2 * speed;
        }
        else
        {
            currentSpeed = speed;
        }
        // Tank controls - Move forward/backward on Z-axis
        float z = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * z * currentSpeed * Time.deltaTime);

        // Rotate on Y axis
        float rotationAxis = Input.GetAxis("Horizontal");
        transform.localRotation = Quaternion.Euler(0, rotationAxis * 360 * torque * Time.deltaTime, 0);
    }

    // Orient the rb along the planet's surface normal
    private void OrientBody()
    {
        Vector3 surfaceNorm = this.transform.position - planet.transform.position;
        surfaceNorm = Vector3.Normalize(surfaceNorm);

        rb.transform.localRotation = Quaternion.FromToRotation(rb.transform.up, surfaceNorm) * rb.rotation;

    }

    private void Collect()
    {

    }


    private void Fire()
    {

    }
}
