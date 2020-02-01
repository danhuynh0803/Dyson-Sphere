using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    //public float amplitude = 2;
    //public float frequency = 2;
    //public float rotationSpeed = 10.0f;
    public Player controllingPlayer = Player.NONE;

    private void Start()
    {
        // Orient location axes with the surface normal of planet to avoid clipping
        transform.rotation = Random.rotation;

        controllingPlayer = Player.NONE;
    }
    private void Update()
    {
        // Make them bounce up and rotate over time
        //transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        //Debug.Log(amplitude * Mathf.Sin(frequency * Time.deltaTime));
        //Vector3 newPosition = new Vector3(0.0f, amplitude * Mathf.Sin(frequency * Time.deltaTime), 0.0f);
        //transform.localPosition = transform.localPosition + newPosition;
    }
}
