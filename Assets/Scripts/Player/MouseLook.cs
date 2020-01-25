using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivityX;
    public float sensitivityY;
    public float minimumX;
    public float maximumX;
    public float minimumY;
    public float maximumY;
    [Tooltip("Default is set to 60 degree by the modeler")]
    public float maximumCharacterHeadRotationY = 60f;

    [Tooltip("Default is set to 90 degree by the modeler")]
    public float maximumCharacterBodyRotationY = 90f;

    [Tooltip("Default is set to 40 degree by the modeler")]
    public float maximumCharacterBodyRotationX = 40f;

    public Animator playerAnimator;
    public float defaultRotationX;
    public float defaultRotationY;
    
    private float rotationX;
    private float rotationY;

    void Start()
    {
        defaultRotationX = GetComponent<Transform>().localEulerAngles.x;
        defaultRotationY = GetComponent<Transform>().localEulerAngles.y;
        rotationX = -1f * defaultRotationX;
        rotationY = defaultRotationY;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotationX += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY += (Input.GetAxis("Mouse X") * sensitivityX);
            GetComponent<Camera>().fieldOfView = 45f;
            playerAnimator.SetFloat("Head_Horizontal_f", rotationY / maximumCharacterHeadRotationY);
            playerAnimator.SetFloat("Body_Horizontal_f", rotationY / maximumCharacterBodyRotationY);
            playerAnimator.SetFloat("Body_Vertical_f", rotationX / maximumCharacterBodyRotationX);
            playerAnimator.SetInteger("WeaponType_int", 1);
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-1f * rotationX, rotationY, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(defaultRotationX, defaultRotationY, transform.localEulerAngles.z);
            playerAnimator.SetInteger("WeaponType_int", 0);
            playerAnimator.SetFloat("Body_Horizontal_f", 0f);
            playerAnimator.SetFloat("Body_Vertical_f", 0f);
            rotationX = -1f * defaultRotationX;
            rotationY = defaultRotationY;
            GetComponent<Camera>().fieldOfView = 60f;
        }
    }
}