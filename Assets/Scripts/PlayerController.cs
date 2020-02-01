using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    public float speed;
    public float torque;
    public GameObject planet;

    [Header("Vacuum parameters")]
    public float suctionAngle = 50;

    Rigidbody rb;
    float currentSpeed;
    Vector3 mRotation;


    public float maxCarriedCount = 3;
    public float decreaseSpeedAmount = 1.5f;
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

        // Lower the speed based off of how many objects the player is carrying
        currentSpeed -= (decreaseSpeedAmount * carriedCount);


        // Tank controls - Move forward/backward on Z-axis
        float z = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * z * currentSpeed * Time.deltaTime);

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


        // Suck up objects in a cone shape in front of the player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SUCC");
            carriedCount++;
        }

    }

    //carriedCount++;

    private void Fire()
    {
        if (carriedCount <= 0)
        {
            return;
        }

        // Shoot out the object
    }
}
