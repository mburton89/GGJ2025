using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Newspaper : MonoBehaviour
{
    public int directHitScore;
    public int maxScore;
    public float maxHitDistance;

    private Collider targetCollider; 

    public int throwScore;

    [Header("Rotation Settings")]
    public float rotationSpeed = 100f; // Rotation speed in degrees per second
    public bool rotateClockwise = true;

    bool hasHitTarget;
    bool hasHitAreaTarget;

    // Start is called before the first frame update
    void Start()
    {
        maxScore = 75;
        maxHitDistance = 5f;
        throwScore = 0;
        //targetCollider = gameObject.CompareTag("Target").gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHitTarget)
        {
            float direction = rotateClockwise ? 1f : -1f;
            transform.Rotate(0, direction * rotationSpeed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !hasHitTarget)
        {
            FindObjectOfType<UIManager>().AddScore(directHitScore);
            hasHitTarget = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Area Target") && !hasHitAreaTarget)
        {
            float hitDistance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            print("Area Target Hit Distance: " + hitDistance);
            throwScore = CalculateScore(hitDistance);
            FindObjectOfType<UIManager>().AddScore(throwScore);
            hasHitAreaTarget = true;
        }
    }

    int CalculateScore(float hitDistance)
    {
        if (hitDistance <= maxHitDistance)
        {
            int calculatedScore = Mathf.RoundToInt((1f - (hitDistance / maxHitDistance)) * maxScore + 25);
            return Mathf.Max(calculatedScore, 0);
        }
        else
        {
            return 0;
        }
    }
}
