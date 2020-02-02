using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    public bool isSprite;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        
    }

    void Update()
    {
        posOffset = transform.position;
        tempPos = posOffset;
        tempPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        if (isSprite)
        {
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            tempPos.z = 0.0f;
        }

        transform.position = tempPos;
    }
}