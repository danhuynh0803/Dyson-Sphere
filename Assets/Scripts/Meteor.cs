using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Meteor: MonoBehaviour
{
    public float speed;
    public Transform planetTransform;
    public GameObject fragment;

    private Vector3 direction;
    private Rigidbody rb;

    // Meteor travel towards planet
    void Start()
    {
        if (planetTransform == null)
        {
            // Automatically find the planet object in scene
            planetTransform = GameObject.FindGameObjectWithTag("Planet").transform;

        }
        rb = GetComponent<Rigidbody>();
        direction = Vector3.Normalize(planetTransform.position - transform.position);
    }

    void FixedUpdate()
    {
        rb.AddForce(speed * direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Planet")
        {
            // TODO spawn a smoke particle

            // Spawn the Fragment
            SpawnFragment();

            Destroy(gameObject);
        }
    }

    void SpawnFragment()
    {
        //Debug.Log("Spawn fragment");
        Instantiate(fragment, transform.position, Quaternion.identity);
    }
}
