using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject newsPaperPrefab; //Newspaper prefab
    public Transform spawnpoint; //Spawn point og prefab


    [Header("Throw Settings")]
    public float throwingForce = 30f;
    public Vector3 leftDirection = new Vector3(-1, 1, 0);
    public Vector3 rightDirection = new Vector3(1, 1, 0);
    public float objectLifeTime;
    private bool canThrow = true;
    public int throwDelay;

    public float upwardVelocity;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && canThrow)
        //{
        //    //left click
        //    Debug.Log("im left");
        //    StartCoroutine(ThrowWithDelay(leftDirection));
            

        //}
        //else if (Input.GetMouseButtonDown(1) && canThrow)
        //{
        //    //right click
        //    Debug.Log("im right");
        //    StartCoroutine(ThrowWithDelay(rightDirection));
          
        //}
    }

    public void ThrowLeft()
    {
        StartCoroutine(ThrowWithDelay(leftDirection));
    }

    public void ThrowRight()
    {
        StartCoroutine(ThrowWithDelay(rightDirection));
    }

    IEnumerator ThrowWithDelay(Vector3 direction)
    {
        Debug.Log("ThrowWithDelay");
        canThrow = false;

        GameObject thrownObject = Instantiate(newsPaperPrefab, spawnpoint.position, Quaternion.identity);

        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("The prefab must have a Rigidbody component!");
            Destroy(thrownObject);
            yield break;

        }

        rb.velocity = Vector3.zero;
        Vector3 throwDirection = direction.normalized * throwingForce;
        Vector3 newThrowDirection = new Vector3(throwDirection.x, upwardVelocity, throwDirection.z);

        rb.AddForce(newThrowDirection, ForceMode.Impulse);
        //rb.AddTorque(new Vector3(newspaperTorqueX, newspaperTorqueY, newspaperTorqueZ));
        Debug.Log("Normaized");
        Destroy(thrownObject,objectLifeTime);

        yield return new WaitForSeconds(throwDelay);
        canThrow = true;

    }
}
