using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    P1,
    P2,
    NONE
};

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Player playerNumber;

    [Header("Movement parameters")]
    public float speed = 25.0f;
    public float torque = 60.0f;
    public GameObject planet;
    public Transform hose;
    [Header("Vacuum parameters")]
    public float suctionAngle = 50;

    public GameObject debris;
    public float constraint;
    Rigidbody rb;
    float currentSpeed;
    Vector3 mRotation;


    public float maxCarriedCount = 3;
    public float decreaseSpeedAmount = 1.5f;
    public float suckDistance = 10f;
    private int carriedCount;

    private Animator anim;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
        carriedCount = 0;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Collect();
        Fire();
    }

    void FixedUpdate()
    {
//        if (playerNumber == Player.P1)
//        {
//            Movement();
//        }
//        else if (playerNumber == Player.P2)
//        {
//            MovementJoystick();
//        }
    }

    private void MovementJoystick()
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
        float z = Input.GetAxis("VerticalJoystick");
        transform.Translate(Vector3.forward * z * currentSpeed * Time.fixedDeltaTime);

        // Rotate on Y axis
        float x = Input.GetAxisRaw("HorizontalJoystick");
        mRotation.z += -1 * Time.fixedDeltaTime * x * torque;
        transform.localRotation = Quaternion.Euler(mRotation);
    }


    // TODO refactor/remove
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
        transform.Translate(Vector3.forward * z * currentSpeed * Time.fixedDeltaTime);

        // Rotate on Y axis
        Debug.Log("Normal Movement()");
        float x = Input.GetAxisRaw("Horizontal");
        Debug.Log("x: " + x);
        Debug.Log("Time.fixedDeltaTime " + Time.fixedDeltaTime);
        Debug.Log("torque " + torque);
        mRotation.z += -1 * Time.fixedDeltaTime * x * torque;
        Debug.Log(mRotation.z);
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
        if ( (Input.GetKeyDown(KeyCode.Space) && playerNumber == Player.P1) ||
             (Input.GetKeyDown("joystick 1 button 0") && playerNumber == Player.P2) )
        {
            // TODO Make it so that this plays only when we hold down space
            int random = Random.Range(0, 4);
            anim.SetTrigger("Suck"); 
            SoundController.Play(random, 0.1f);
            GameObject[] fragments = GameObject.FindGameObjectsWithTag("Fragment");
            Debug.Log(fragments.Length);
            foreach (var fragment in fragments)
            {
                Debug.Log(Vector3.Distance(hose.position, fragment.transform.position));
                if (Vector3.Distance(hose.position, fragment.transform.position) <= suckDistance)
                {
                    carriedCount++;
                    Destroy(fragment);
                }
            }
        }
    }

    private void Fire()
    {
        //if (carriedCount <= 0)
        //{
        //    return;
        //}
        if ( (Input.GetKeyDown(KeyCode.Q) && playerNumber == Player.P1) ||
             (Input.GetKeyDown("joystick 1 button 1") && playerNumber == Player.P2) )
        {
            SoundController.Play(6, 0.1f);
            GameObject debris = Instantiate(this.debris, transform.position, transform.rotation);
            debris.GetComponent<Orbiting>().planet = planet.transform;
            debris.GetComponent<Orbiting>().controllingPlayer = playerNumber;
            debris.GetComponent<Orbiting>().vaccum = transform;
            //Debug.Log(transform.rotation.z);
            carriedCount--;
        }
    }

    void LateUpdate()
    {
//        if (Vector3.Distance(transform.position, planet.transform.position) > constraint || Vector3.Distance(transform.position, planet.transform.position) < constraint)
//        {
//            transform.position = constraint * (transform.position - planet.transform.position).normalized;
//        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Fragment>())
        {
            //Debug.Log("Carrying fragment!");
            carriedCount++;
            Destroy(other.gameObject);
        }
    }
}
