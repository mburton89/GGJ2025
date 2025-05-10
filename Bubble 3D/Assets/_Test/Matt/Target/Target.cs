using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetParent targetParent;
    [HideInInspector] public bool hasBeenHit;
    public float carHitForce = 800f;

    bool hasBeenHitByCar;
    public void HandleHit()
    { 
        hasBeenHit = true;
        targetParent.DisableTargets();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MovementController>() && !hasBeenHitByCar)
        {
            HandleHitByCar(collision.gameObject);
        }
    }

    public void HandleHitByCar(GameObject car)
    {
        if (!hasBeenHit)
        {
            PopupTextManager.instance.ShowPopupText(transform, "Delivery Failed");
        }
        SoundManager.Instance.PlayPunchSound();
        HandleHit();
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce((transform.position - car.transform.position) * (carHitForce * SoundManager.Instance.motorSound.volume));
        hasBeenHitByCar = true;
    }
}
