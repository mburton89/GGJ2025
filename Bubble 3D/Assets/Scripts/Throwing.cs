using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject newsPaperPrefab; //Newspaper prefab
    public Transform spawnpoint; //Spawn point og prefab


    [Header("Throw Settings")]
    public float throwingForce = 20f;
    public Vector3 leftDirection = new Vector3(-1, 1, 0);
    public Vector3 rightDirection = new Vector3(1, 1, 0);
    public float objectLifeTime;
    private bool canThrow;
    public int throwDelay;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            //left click
            StartCoroutine(ThrowWithDelay(leftDirection));
            Debug.Log("im left");

        }
        else if (Input.GetMouseButtonDown(1) && canThrow)
        {
            //right click
            StartCoroutine(ThrowWithDelay(rightDirection));
            Debug.Log("im right");


        }

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

        rb.velocity = Vector3.zero;
        rb.AddForce(direction.normalized * throwingForce, ForceMode.Impulse);

        Destroy(thrownObject,objectLifeTime);

        yield return new WaitForSeconds(throwDelay);
        canThrow = true;

    }
}
