using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcModel : MonoBehaviour
{
    [Tooltip("List of possible NPC model prefabs.")]
    public List<GameObject> npcModels;

    [Tooltip("Parent GameObjects for each NPC.")]
    public List<GameObject> npcParents;

    void Start()
    {
        RandomizeModels();
    }

    private void RandomizeModels()
    {
        if (npcModels.Count == 0 || npcParents.Count == 0)
        {
            Debug.LogWarning("NPC Models or Parents list is empty!");
            return;
        }

        foreach (GameObject parent in npcParents)
        {
            if (parent == null) continue;

            // Remove existing children
            foreach (Transform child in parent.transform)
            {
                Destroy(child.gameObject);
            }

            // Randomly select a model
            GameObject randomModel = npcModels[Random.Range(0, npcModels.Count)];

            // Instantiate and set the model as a child of the parent
            GameObject newModel = Instantiate(randomModel, parent.transform);
            newModel.transform.localPosition = Vector3.zero;
            newModel.transform.localRotation = Quaternion.identity;
        }
    }
}
