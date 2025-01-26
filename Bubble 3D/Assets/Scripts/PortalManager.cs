using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ActivatePortal(string s_PortalName)
    {
        print("In");
        if (s_PortalName.Contains("Memphish"))
        {
            print("ToMemphish");
            SceneManager.LoadScene("M_HazardTest_PortalB");
            SpawnPlayerAtPortal(s_PortalName);
        } else if (s_PortalName.Contains("FishHell"))
        {
            print("ToFishHell");
            SceneManager.LoadScene("M_HazardTest");
            SpawnPlayerAtPortal(s_PortalName);
        } else
        {
            print("Something is going very wrong");
        }
        
    }

    public void SpawnPlayerAtPortal(string s_PortalName)
    {
        var portals = FindObjectsOfType<PortalSystem>();

        foreach (var portal in portals)
        {
            if (portal.name == s_PortalName)
            {
                var playerCar = FindFirstObjectByType<MovementController>();
                playerCar.transform.parent.position = portal.v3_EjectPoint;
                playerCar.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0) * 10, ForceMode.Impulse);
                break;
            }
        }
    }

}
