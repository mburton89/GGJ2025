using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ComicText : MonoBehaviour
{
    public float zRotation;
    public float amountToRotate;

    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float randOffset;

    public TextMeshProUGUI text;

    public float upwardMovementSpeed;
    public float upwardAcceleration;

    public float maxMovementSpeed = 200f;

    int timesCalled;

    public void Init(string newText, Transform spawnPosition)
    {
        GetComponentInParent<Canvas>().sortingOrder = 10;

        text.SetText(newText);

        text.transform.eulerAngles = new Vector3(0, 0, Random.Range(-zRotation, zRotation));

        transform.position = new Vector3(spawnPosition.position.x + xOffset, spawnPosition.position.y, spawnPosition.position.z);

        //float randX = Random.Range(-randOffset, randOffset);
        //float randY = Random.Range(-randOffset, randOffset);
        //Vector3 randVector2Offset = new Vector3(randX, randY, 0);

        //transform.position += randVector2Offset;

        //if (transform.position.y > 2.5f)
        //{
        //    transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        //}

        transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.25f, 10, 1);

        Vector3 newRotate = new Vector3(0, 0, Random.Range(-amountToRotate, amountToRotate));
        text.transform.DORotate(newRotate, 2, RotateMode.Fast).SetEase(Ease.OutQuad);

        if (spawnPosition.position.y < 3.5f)
        {
            text.transform.DOMoveY(transform.position.y + yOffset, .25f).SetEase(Ease.OutQuad);
        }

        StartCoroutine(FadeCo());

        Destroy(gameObject, 2.2f);

        timesCalled++;
    }

    public void Init(string newText, Transform spawnPosition, bool doubleSize)
    {
        Init(newText, spawnPosition);
        text.transform.localScale *= 2;
    }

    private IEnumerator FadeCo()
    {
        yield return new WaitForSeconds(1);
        text.DOFade(0, 2);
    }

    private void FixedUpdate()
    {
        if (upwardMovementSpeed < maxMovementSpeed)
        {
            upwardMovementSpeed *= upwardAcceleration;
        }
        else
        {
            upwardMovementSpeed = maxMovementSpeed;
        }

        transform.Translate(Vector3.up * upwardMovementSpeed);
        //transform.position -= Vector3.left * leftMovementSpeed;
    }

    public void UpdateText(string newText, int fuelGain)
    { 
        timesCalled++;

        int newFuelGain = fuelGain * timesCalled;

        text.SetText(newText + " x" + timesCalled + "\n+" + newFuelGain + " Fuel");

        //text.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        if (timesCalled > 2)
        {
            text.transform.DOScale(text.transform.localScale.x + .125f, .0125f);
        }
    }

    public void UpdateDamageText(int damage)
    {
        timesCalled++;

        int newDamage = damage * timesCalled;

        text.SetText(newDamage + " Damage");

        //text.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        if (timesCalled > 2)
        {
            text.transform.DOScale(text.transform.localScale.x + .125f, .0125f);
        }
    }
}
