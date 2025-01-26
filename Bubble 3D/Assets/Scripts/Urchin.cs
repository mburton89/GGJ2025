using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urchin : MonoBehaviour
{

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //other.GetComponent<Rigidbody>().AddForce(Vector3.zero, ForceMode.VelocityChange);
        }
    }


}
