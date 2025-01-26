using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GrowAndShrink : MonoBehaviour
{
    public float secondsBetweenSizeChange;
    public float maxSize;
    Coroutine coroutine;

    void OnEnable()
    {
        coroutine = StartCoroutine(GrowAndShrinkCo());
    }

    void OnDisable()
    {
        if (coroutine != null)
        { 
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator GrowAndShrinkCo()
    {
        transform.DOScale(new Vector3(maxSize, maxSize, 1), secondsBetweenSizeChange).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsBetweenSizeChange);
        transform.DOScale(1, secondsBetweenSizeChange).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsBetweenSizeChange);
        coroutine = StartCoroutine(GrowAndShrinkCo());
    }
}
