using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1200f;
    [SerializeField]
    private float mouseSensitivity = 2f;
    [SerializeField]
    private float mousePitchRange = 90f;

    [SerializeField]
    private float jumpStrength = 20000f;

    private float pitchRot = 0f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        // Calculate movement velocity as Vector3    
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;
        
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * movementSpeed;
        motor.SetVelocity(velocity);

        // Player yaw rotation
        float yawRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yawRot, 0f) * mouseSensitivity;

        motor.SetPlayerRotation(rotation);

        // Camera pitch rotation
        pitchRot -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        pitchRot = Mathf.Clamp(pitchRot, -mousePitchRange, mousePitchRange);
        motor.SetCameraRotation(pitchRot);

        // Jump
        Vector3 jumpVector = Vector3.zero;
        if (Input.GetButtonDown("Jump") && motor.IsGrounded())
        {
            jumpVector = Vector3.up * jumpStrength;
        }

        motor.SetJumpForce(jumpVector);
    }

    
}
