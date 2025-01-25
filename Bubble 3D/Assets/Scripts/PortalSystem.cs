using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    
    public Vector3 v3_EjectPoint;
    public PortalSystem ps_PartnerPortal;

    public void Awake()
    {
        v3_EjectPoint = transform.GetChild(0).position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().position = ps_PartnerPortal.v3_EjectPoint;
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
        }
    }

}
