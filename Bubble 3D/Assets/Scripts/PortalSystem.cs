using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    
    public Vector3 v3_EjectPoint;
    public string s_TargetPortalName;
    public PortalManager pm_PortalManager;

    public void Awake()
    {
        v3_EjectPoint = transform.GetChild(0).position;
        pm_PortalManager = FindObjectOfType<PortalManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            pm_PortalManager.ActivatePortal(s_TargetPortalName);
        } else
        {
            print("Something went wrong");
        }
    }



}
