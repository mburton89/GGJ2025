using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishCitizen : MonoBehaviour
{
    public GameObject fishCitizenPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<UIManager>().AddScore(100);
        }
    }


}
