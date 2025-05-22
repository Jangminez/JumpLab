using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 moveDistance;
    [SerializeField] float duration;
    bool isMoved = false;
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

                transform.position = Vector3.Lerp(startPos, targetPos, t);
                yield return null;
            }

            transform.position = targetPos;
        }
    }
}
