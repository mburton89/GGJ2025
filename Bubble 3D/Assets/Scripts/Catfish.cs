using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catfish : MonoBehaviour
{
    public int i_AttachLifetime;
    public float f_ScoreMod;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            this.transform.parent = other.transform;
        }
    }
}
