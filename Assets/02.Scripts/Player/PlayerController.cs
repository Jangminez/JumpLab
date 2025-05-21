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

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.right * 0.25f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.25f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.forward * 0.25f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.25f) + (transform.up * 0.1f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    #region InputSystem
    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnLook(InputValue inputValue)
    {
        mouseDelta = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && IsGrounded())
        {
            var velocity = _rigidbody.velocity;
            velocity.y = 0;
            _rigidbody.velocity = velocity;

            _rigidbody.AddForce(Vector3.up * playerStats.JumpForce, ForceMode.Impulse);
            player.UseStamina(jumpStamina);
        }
    }

    void OnInteract(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            player.InteractItem();
        }
    }

    void OnUseItem(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            player.UseItem();
        }
    }
    #endregion
}
