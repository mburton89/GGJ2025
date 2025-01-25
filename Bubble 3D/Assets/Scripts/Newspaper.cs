using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Newspaper : MonoBehaviour
{
    public int directHitScore;
    public int maxScore;
    public int minScore = 25;
    public float maxHitDistance;

    private Collider targetCollider; 

    public int throwScore;

    [Header("Rotation Settings")]
    public float rotationSpeed = 100f; // Rotation speed in degrees per second
    public bool rotateClockwise = true;

    bool hasHitTarget;
    //bool hasHitAreaTarget;

    public float hitForce = 200f;

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
        if (collision.gameObject.CompareTag("Target") && !collision.gameObject.GetComponent<Target>().hasBeenHit)
        {
            FindObjectOfType<UIManager>().AddScore(directHitScore);
            hasHitTarget = true;
            PopupTextManager.instance.ShowPopupText(collision.gameObject.transform, "Direct Hit!\n" + directHitScore + " points!");
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position) * hitForce);
            SoundManager.Instance.PlayPunchSound();
            SoundManager.Instance.PlayDirectHitSound();
            collision.gameObject.GetComponent<Target>().HandleHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Area Target") && !other.gameObject.GetComponent<Target>().hasBeenHit)
        {
            float hitDistance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            print("Area Target Hit Distance: " + hitDistance);
            throwScore = CalculateScore(hitDistance);

            if (throwScore < minScore)
            {
                throwScore = minScore;
            }

            FindObjectOfType<UIManager>().AddScore(throwScore);
            PopupTextManager.instance.ShowPopupText(other.gameObject.transform, throwScore + " points!");
            SoundManager.Instance.PlayScorePointSound();
            other.gameObject.GetComponent<Target>().HandleHit();
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
