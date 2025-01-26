using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urchin : MonoBehaviour
{

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            print("In");
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            var children = other.GetComponentsInChildren<Transform>();
            

            foreach (var child in children)
            {
                if (child.name == "Fish Pointer")
                {
                    print("Deepers");
                    
                    break;
                }
            }

            other.gameObject.SetActive(false);
            

        }
    }


}
