using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bob : MonoBehaviour
{
    //public float maxYPos;
    //public float minYPos;
    public float secondsToMove;
    // Start is called before the first frame update

    public float distanceToMoveUp;
    float initialYPosition;

    Coroutine coroutine;
    Tween tween;

    void OnEnable()
    {
        initialYPosition = transform.localPosition.y;
        coroutine = StartCoroutine(BobCo());   
    }

    void Disable()
    {
        transform.localPosition = new Vector3(transform.position.x, initialYPosition, transform.position.z);

        if(tween != null ) 
        {
            tween.Kill();
            tween = null;
        }

        if (coroutine != null)
        { 
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator BobCo()
    {
        tween = transform.DOLocalMoveY(initialYPosition + distanceToMoveUp, secondsToMove, false).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsToMove);
        tween =transform.DOLocalMoveY(initialYPosition, secondsToMove, false).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsToMove);
        coroutine = StartCoroutine(BobCo());
    }
}
