using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public float speed;
    public float torque;
    public GameObject planet;

    Rigidbody rb;
    float currentSpeed;
    Vector3 mRotation;

    private int carriedCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
        carriedCount = 0;
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
        transform.Translate(Vector3.forward * z * currentSpeed * Time.fixedDeltaTime);

        // Rotate on Y axis
        float x = Input.GetAxisRaw("Horizontal");
        mRotation.y += Time.deltaTime * x * torque;
        transform.localRotation = Quaternion.Euler(mRotation);
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
        // Use suction to absorb
        // Absorb

        carriedCount++;

    }


    private void Fire()
    {
        if (carriedCount <= 0)
        {
            return;
        }

        // Shoot out the object
    }

    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, planet.transform.position) > 5.0f)
        {
            transform.position = 5.0f * (transform.position - planet.transform.position).normalized;
        }
    }
}
