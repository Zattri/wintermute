using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    // Movement
    [SerializeField]
    float fallMultiplier = 2.5f;
    [SerializeField]
    float lowJumpMultiplier = 2f;
    private Vector3 velocity = Vector3.zero;

    private Vector3 jumpForce = Vector3.zero;

    // Camera
    private Vector3 playerRotation = Vector3.zero;
    private float cameraPitchRotation = 0.0f;
    
    private Camera cam;
    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    // Runs every physics itteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    public void SetVelocity(Vector3 velocityInput)
    {
        velocity = velocityInput;
    }

    private void PerformMovement()
    {
        Debug.Log(velocity);
        if (velocity != Vector3.zero)
        {
            rb.AddForce(velocity * Time.fixedDeltaTime);
        }
        //if (jumpForce != Vector3.zero)
        //{
        //    Debug.Log("Jump force");
        //    rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        //}
    }

    public void SetPlayerRotation(Vector3 rotationInput)
    {
        playerRotation = rotationInput;
    }

    public void SetCameraRotation(float pitchRotation)
    {
        cameraPitchRotation = pitchRotation;
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(playerRotation));
        if (cam != null) {
            cam.transform.localRotation = Quaternion.Euler(cameraPitchRotation, 0, 0);
        }
    }

    public void SetJumpForce(Vector3 jumpVector)
    {
        velocity += jumpVector;
    }

    public void PerformVerticalJump()
    {
        //if (velocity.y < 0)
        //{
        //    Debug.Log("Top");
        //    velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //else if (velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    Debug.Log("Bottom");
        //    velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}
    }
}
