using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Newspaper : MonoBehaviour
{
    public int maxScore;
    public float maxHitDistance;
    public GameObject targetPrefab;

    public int throwScore;


    // Start is called before the first frame update
    void Start()
    {
        maxScore = 100;
        maxHitDistance = 5f;
        throwScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            float hitDistance = Vector3.Distance(collision.GetContact(0).point, collision.transform.position);
            throwScore = CalculateScore(hitDistance);
            FindObjectOfType<UIManager>().AddScore(throwScore);
        }
    }

    int CalculateScore(float hitDistance)
    {
        if (hitDistance <= maxHitDistance)
        {
            int calculatedScore = Mathf.RoundToInt((1f - (hitDistance / maxHitDistance)) * maxScore);
            return Mathf.Max(calculatedScore, 0);
        }
        else
        {
            return 0;
        }
    }
}
