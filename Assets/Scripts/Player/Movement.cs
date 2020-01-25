using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speedX;
    public float rotateZ;
    public Animator animator;
    public Rigidbody rigidBody;
    void Update()
    {
        if(!Input.GetMouseButton(1))
        {
            float InputX = Input.GetAxis("Vertical");
            animator.SetFloat("Speed_f", InputX >= 0 ? InputX : 0.3f);
            animator.SetFloat("Head_Horizontal_f", Input.GetAxis("Horizontal"));
            float fowardSpeed = InputX >= 0 ? InputX * speedX : -1f * speedX * 0.25f;
            rigidBody.AddForce(fowardSpeed * new Vector3(
                Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.PI / 180f),
                0f,
                Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.PI / 180f)) * Time.deltaTime, ForceMode.Impulse);
            transform.eulerAngles += new Vector3(0f, Input.GetAxis("Horizontal") * rotateZ * Time.deltaTime, 0f);
        }
        else
        {
            animator.SetFloat("Speed_f", 0f);
        }
    }
}
