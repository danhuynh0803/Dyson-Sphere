using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour {

    public float speed;
    public float constraint;
    public Transform planet;
    //public float fireAngle;
    Rigidbody rb;
    public Transform vaccum;
    public Player controllingPlayer;
    public float destroyTimer = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(speed * (vaccum.transform.forward * -1), ForceMode.VelocityChange);
        Destroy(gameObject, destroyTimer);
    }

    void FixedUpdate()
    {
        Vector3 centripetalForce = Mathf.Pow(rb.velocity.magnitude, 2) / (planet.position - transform.position).magnitude * (planet.position - transform.position).normalized * Time.fixedDeltaTime;
        rb.AddForce(centripetalForce, ForceMode.VelocityChange);
        transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.forward * -1);
    }

    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, planet.transform.position) > constraint || Vector3.Distance(transform.position, planet.transform.position) < constraint)
        {
            transform.position = constraint * (transform.position - planet.transform.position).normalized;
        }
    }

}
