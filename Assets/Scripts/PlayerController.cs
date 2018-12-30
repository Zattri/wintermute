using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 1.0f;
    public float upDownRange = 90.0f;
    public Camera playerCamera;

    float mousePitch = 0;


    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        float mouseYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseYaw, 0);

        mousePitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mousePitch = Mathf.Clamp(mousePitch, -upDownRange, upDownRange);
        playerCamera.transform.localRotation = Quaternion.Euler(mousePitch, 0, 0);

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        // Movement
        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;
        cc.SimpleMove(speed);
    }
}
