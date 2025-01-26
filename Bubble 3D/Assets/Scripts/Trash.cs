using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int i_AttachLifetime;
    public int i_timer;
    public float f_ScoreMod;

    public bool b_Attached = false;

    public void Awake()
    {
        i_AttachLifetime = i_AttachLifetime * 20;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true && !b_Attached)
        {
            this.transform.parent = other.transform;
            b_Attached = true;

            transform.LookAt(transform.parent.position);
        }
    }

    public void FixedUpdate()
    {
        if (b_Attached)
        {
            transform.RotateAround(transform.parent.position, transform.forward, 40 * Time.deltaTime);
            i_timer++;
        }
        if (i_timer >= i_AttachLifetime)
        {
            Destroy(gameObject);
        }
    }

}
