using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    P1,
    P2
};

public class Fragment : MonoBehaviour
{
    public int pointValue = 1;
    public float amplitude = 2;
    public float frequency = 2;
    public float rotationSpeed = 10.0f;
    private Player player;

    private void Start()
    {
        // Orient location axes with the surface normal of planet to avoid clipping
    }
    private void Update()
    {
        // Make them bounce up and rotate over time
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        //Debug.Log(amplitude * Mathf.Sin(frequency * Time.deltaTime));
        //Vector3 newPosition = new Vector3(0.0f, amplitude * Mathf.Sin(frequency * Time.deltaTime), 0.0f);
        //transform.localPosition = transform.localPosition + newPosition;
    }

    public void SetPlayer(Player controllingPlayer)
    {
        player = controllingPlayer;
    }
}
