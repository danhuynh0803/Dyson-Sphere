using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaymondTesting : MonoBehaviour
{
    public float radius = 0.6f;
    public float translateSpeed = 180.0f;
    public float rotateSpeed = 360.0f;
    public bool isCamera;
    float angle = 0.0f;
    Vector3 direction = Vector3.one;
    Quaternion rotation = Quaternion.identity;

    void Update()
    {
        direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
        // Rotate with left/right arrows
        if (Input.GetKey(KeyCode.LeftArrow)) Rotate(rotateSpeed);
        if (Input.GetKey(KeyCode.RightArrow)) Rotate(-rotateSpeed);

        // Translate forward/backward with up/down arrows
        if (Input.GetKey(KeyCode.UpArrow)) Translate(0, translateSpeed);
        if (Input.GetKey(KeyCode.DownArrow)) Translate(0, -translateSpeed);

        // Translate left/right with A/D. Bad keys but quick test.
        if (Input.GetKey(KeyCode.A)) Translate(translateSpeed, 0);
        if (Input.GetKey(KeyCode.D)) Translate(-translateSpeed, 0);

        UpdatePositionRotation();
    }

    void Rotate(float amount)
    {
        angle += amount * Mathf.Deg2Rad * Time.deltaTime;
    }

    void Translate(float x, float y)
    {
        var perpendicular = new Vector3(-direction.y, direction.x);
        var verticalRotation = Quaternion.AngleAxis(y * Time.deltaTime, perpendicular);
        var horizontalRotation = Quaternion.AngleAxis(x * Time.deltaTime, direction);
        rotation *= horizontalRotation * verticalRotation;
    }

    void UpdatePositionRotation()
    {
        transform.localPosition = rotation * Vector3.forward * radius;
        if(isCamera)
        {
            transform.rotation = rotation * Quaternion.LookRotation(direction, Vector3.forward) * Quaternion.AngleAxis(90f, Vector3.right);
        }
        else
        {
            transform.rotation = rotation * Quaternion.LookRotation(direction, Vector3.forward) * Quaternion.AngleAxis(-180f, Vector3.right);
        }

    }
}
