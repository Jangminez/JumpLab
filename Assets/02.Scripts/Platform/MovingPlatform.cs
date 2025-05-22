using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 moveDistance;
    [SerializeField] float duration;
    bool isMoved = false;
    List<Collider> contactObjects = new List<Collider>();

    void Start()
    {
        StartCoroutine(MovePlatformCoroutine());
    }

    IEnumerator MovePlatformCoroutine()
    {
        while (true)
        {
            isMoved = !isMoved;
            Vector3 startPos = transform.position;
            Vector3 targetPos = isMoved ? transform.position + moveDistance : transform.position - moveDistance;

            float elapse = 0f;

            while (elapse < duration)
            {
                elapse += Time.deltaTime;
                float t = Mathf.Clamp01(elapse / duration);

                Vector3 newPos = Vector3.Lerp(startPos, targetPos, t);
                Vector3 deltaPos = newPos - transform.position;

                transform.position = newPos;

                foreach (var obj in contactObjects)
                {
                    Rigidbody rb = obj.attachedRigidbody;

                    if (rb != null)
                        rb.MovePosition(rb.position + deltaPos);
                }

                yield return null;
            }

            transform.position = targetPos;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        contactObjects.Add(col.collider);
    }

    void OnCollisionExit(Collision col)
    {
        contactObjects.Remove(col.collider);
    }
}
