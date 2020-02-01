using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour {

    public float speedX;
    public float speedY;
    public float speedZ;
    public Transform planet;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(speedX, speedY, speedZ), ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        Vector3 centripetalForce = Mathf.Pow(rb.velocity.magnitude, 2) / (planet.position - transform.position).magnitude * (planet.position - transform.position).normalized * Time.fixedDeltaTime;
        rb.AddForce(centripetalForce, ForceMode.VelocityChange);
        transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up) * (Quaternion.AngleAxis(90f, Vector3.right));
    }

}