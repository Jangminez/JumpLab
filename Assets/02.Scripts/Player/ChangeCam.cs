using System.Collections;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    [SerializeField] Transform firstPersonTr;
    [SerializeField] Transform thirdPersonTr;

    [SerializeField] float duration = 0.5f;

    private Transform mainCam;
    private bool isFirstPerson = true;
    private bool isTransitionEnd = true;

    void Awake()
    {
        mainCam = Camera.main.transform;
    }

    public void ChangeCamera()
    {
        if (isTransitionEnd)
        {
            StartCoroutine(ChangeCameraPositionCoroutine());
            isTransitionEnd = false;
        }
    }

    IEnumerator ChangeCameraPositionCoroutine()
    {
        isFirstPerson = !isFirstPerson;
        Transform target = isFirstPerson ? firstPersonTr : thirdPersonTr;

        Vector3 startPos = mainCam.position;
        Quaternion startRot = mainCam.rotation;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            mainCam.position = Vector3.Lerp(startPos, target.position, t);
            mainCam.rotation = Quaternion.Slerp(startRot, target.rotation, t);

            yield return null;
        }

        mainCam.position = target.position;
        mainCam.rotation = target.rotation;

        isTransitionEnd = true;
    }
}
