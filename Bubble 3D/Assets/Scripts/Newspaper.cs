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
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
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
        print("collision.gameObject.name: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);

        if (collision.gameObject.GetComponent<Target>())
        {
            SoundManager.Instance.PlayPunchSound();
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position) * hitForce);

            if (!collision.gameObject.GetComponent<Target>().hasBeenHit)
            {
                FindObjectOfType<UIManager>().AddScore(directHitScore);
                hasHitTarget = true;
                PopupTextManager.instance.ShowPopupText(collision.gameObject.transform, "Direct Hit!\n" + directHitScore + " points!\n" + GameTimer.Instance.timeToAdd + "seconds");
                SoundManager.Instance.PlayDirectHitSound();
                GameTimer.Instance.AddTime();
                collision.gameObject.GetComponent<Target>().HandleHit();
            }
        }

        GetComponent<AudioSource>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("other.gameObject.name: " + other.gameObject.name + " Tag: " + other.gameObject.tag); 

        if (other.gameObject.GetComponent<Target>() && !other.gameObject.GetComponent<Target>().hasBeenHit)
        {
            float hitDistance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            print("Area Target Hit Distance: " + hitDistance);
            throwScore = CalculateScore(hitDistance);

            if (throwScore < minScore)
            {
                throwScore = minScore;
            }

            FindObjectOfType<UIManager>().AddScore(throwScore);
            PopupTextManager.instance.ShowPopupText(other.gameObject.transform, throwScore + " points!\n" + GameTimer.Instance.timeToAdd + "seconds");
            SoundManager.Instance.PlayScorePointSound();
            GameTimer.Instance.AddTime();
            other.gameObject.GetComponent<Target>().HandleHit();
        }

        GetComponent<AudioSource>().Stop();
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
