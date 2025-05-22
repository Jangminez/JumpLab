using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Player player;
    private PlayerStats playerStats;

    [Header("Movement")]
    [SerializeField] float jumpStamina;
    [SerializeField] LayerMask groundLayerMask;
    private Vector2 moveInput;
    private int jumpCount;

    [Header("Look")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float minXLook;
    [SerializeField] float maxXLook;
    [SerializeField] float lookSensitivity;
    private float camCurXRot;
    private Vector2 mouseDelta;

    public void Init(Player player, PlayerStats playerStats)
    {
        this.player = player;
        this.playerStats = playerStats;

        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = direction.x * playerStats.MoveSpeed;
        velocity.z = direction.z * playerStats.MoveSpeed;

        _rigidbody.velocity = velocity;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.Rotate(Vector3.up * mouseDelta.x * lookSensitivity);
    }

    void Jump()
    {
        if (jumpStamina <= playerStats.Stamina && jumpCount < playerStats.MaxJumpCount)
        {
            var velocity = _rigidbody.velocity;
            velocity.y = 0;
            _rigidbody.velocity = velocity;

            _rigidbody.AddForce(Vector3.up * playerStats.JumpForce, ForceMode.Impulse);
            player.UseStamina(jumpStamina);
            jumpCount++;
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.right * 0.1f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.1f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.forward * 0.1f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.1f) + (transform.up * 0.05f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    #region InputSystem
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            moveInput = context.ReadValue<Vector2>();

        if (context.phase == InputActionPhase.Canceled)
            moveInput = Vector2.zero;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                jumpCount = 0;
            }

            Jump();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            player.InteractItem();
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            player.UseItem();
        }
    }

    public void OnChangeCam(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (cameraContainer.TryGetComponent(out ChangeCam changeCam))
            {
                changeCam.ChangeCamera();
            }
        }
    }
    #endregion
}
