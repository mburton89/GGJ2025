using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTunnel : MonoBehaviour
{
    public float f_CurrentStrength;
    public int i_ApplicationRate;
    public int i_timer;


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            if (i_timer >= i_ApplicationRate)
            {
                Vector3 v3_AppliedForce = this.transform.up;
                other.GetComponent<Rigidbody>().AddForce(v3_AppliedForce.normalized * f_CurrentStrength, ForceMode.Impulse);
                i_timer = 0;
            } else
            {
                i_timer++;
            }

        }
    }

}
