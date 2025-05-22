using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] float jumpForce;

    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = col.collider.attachedRigidbody;
        
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
