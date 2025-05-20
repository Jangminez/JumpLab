using System.Collections;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    public AnimationCurve popupCurve;
    public float animationDuration = 0.5f;

    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void ShowPopup()
    {
        StopAllCoroutines();
        StartCoroutine(PopupRoutine());
    }

    IEnumerator PopupRoutine()
    {
        float time = 0f;

        while (time < animationDuration)
        {
            float t = time / animationDuration;
            float curveValue = popupCurve.Evaluate(t);
            transform.localScale = originalScale * curveValue;

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}


