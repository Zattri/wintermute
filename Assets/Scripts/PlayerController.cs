using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 700f;
    [SerializeField]
    private float mouseSensitivity = 2f;
    [SerializeField]
    private float mousePitchRange = 90.0f;

    [SerializeField]
    private float jumpStrength = 1000f;

    private float pitchRot = 0.0f;

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

        // Final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
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
        if (Input.GetButtonDown("Jump"))
        {
            jumpVector = Vector3.up * jumpStrength * 20;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            jumpVector = new Vector3(0, 0.5f, -1.5f) * jumpStrength * 20;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            jumpVector = new Vector3(0, 0.5f, 1.5f) * jumpStrength * 20;
        }

        motor.SetJumpForce(jumpVector);
    }
}
