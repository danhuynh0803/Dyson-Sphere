using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaymondTesting : MonoBehaviour
{
    public float radius = 0.6f;
    public float translateSpeed = 180.0f;
    public float rotateSpeed = 360.0f;
    public bool isCamera;
    public float radian = 0.0f;
    public float degree = 0.0f;
    Vector3 direction = Vector3.one;
    Quaternion rotation = Quaternion.identity;

    void Update()
    {
        direction = new Vector3(Mathf.Sin(radian), Mathf.Cos(radian));
        // Rotate with left/right arrows
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) Rotate(rotateSpeed);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) Rotate(-rotateSpeed);

        // Translate forward/backward with up/down arrows
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) Translate(0, translateSpeed);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) Translate(0, -translateSpeed);

        UpdatePositionRotation();
    }

    void Rotate(float amount)
    {
        radian += amount * Mathf.Deg2Rad * Time.deltaTime;
        degree = radian * Mathf.Rad2Deg;
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
