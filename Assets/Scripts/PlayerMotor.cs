using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    // Movement
    [SerializeField]
    float fallMultiplier = 2.5f;
    [SerializeField]
    float lowJumpMultiplier = 2f;
    [SerializeField]
    float maxSpeed = 700;
    
    public LayerMask groundLayers;

    private Vector3 velocity = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;

    // Camera
    private Vector3 playerRotation = Vector3.zero;
    private float cameraPitchRotation = 0.0f;
    
    private Camera cam;
    private Rigidbody rb;
    private CapsuleCollider col;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        col = GetComponent<CapsuleCollider>();
    }

    // Runs every physics itteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        PerformVerticalJump();
    }

    public void SetVelocity(Vector3 velocityInput)
    {
        velocity = velocityInput;
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.AddForce(velocity * Time.fixedDeltaTime);
        }
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
        if (velocity.y < 0)
        {
            velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump"))
        {
            velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public bool IsGrounded()
    {
        return Physics.CheckCapsule(
            col.bounds.center, 
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), 
            col.radius * .9f,
            groundLayers);
    }
}
