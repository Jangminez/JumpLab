using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] float jumpForce;

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
