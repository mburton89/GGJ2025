using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Check if another instance of this object already exists
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // If it does, destroy this one to avoid duplicates
            Destroy(gameObject);
            return;
        }

        // Make this object persist across scene loads
        DontDestroyOnLoad(gameObject);
    }
}
