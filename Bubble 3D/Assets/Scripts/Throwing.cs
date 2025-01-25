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

    public void ThrowForward()
    {
        StartCoroutine(ThrowWithDelay(transform.forward));
    }

    IEnumerator ThrowWithDelay(Vector3 direction)
    {
        canThrow = false;

        GameObject thrownObject = Instantiate(newsPaperPrefab, spawnpoint.position, Quaternion.identity);

        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("The prefab must have a Rigidbody component!");
            Destroy(thrownObject);
            yield break;
        }

        if (GetComponent<CharacterController>())
        { 
            rb.velocity = GetComponent<CharacterController>().velocity;
        }
        else if (GetComponent<Rigidbody>())
        {
            rb.velocity = GetComponent<Rigidbody>().velocity;
        }

        Vector3 throwDirection = direction.normalized * throwingForce;

        // Calculate throw direction relative to the character's facing direction
        Vector3 relativeThrowDirection = transform.TransformDirection(new Vector3(throwDirection.x, 0, throwDirection.z));
        //Vector3 relativeThrowDirection = transform.forward * throwingForce;

        // Add upward velocity to the throw direction
        Vector3 newThrowDirection = new Vector3(relativeThrowDirection.x, upwardVelocity, relativeThrowDirection.z);
        //Vector3 newThrowDirection = new Vector3(relativeThrowDirection.x, upwardVelocity, relativeThrowDirection.z);

        rb.AddForce(newThrowDirection, ForceMode.Impulse);
        //rb.AddTorque(new Vector3(newspaperTorqueX, newspaperTorqueY, newspaperTorqueZ));
        Debug.Log("Normaized");
        Destroy(thrownObject,objectLifeTime);

        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }
}
