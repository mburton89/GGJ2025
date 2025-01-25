using UnityEngine;

public class TargetScatter : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject[] prefabs; // Array of prefabs to scatter
    public Terrain terrain; // Reference to the terrain
    public int numberOfPrefabs = 40; // Number of prefabs to scatter

    [Header("Placement Settings")]
    public float minSpacing = 10f; // Minimum spacing between prefabs

    public Transform parent;
    void Start()
    {
        ScatterPrefabs();
    }

    void ScatterPrefabs()
    {
        if (terrain == null || prefabs.Length == 0)
        {
            Debug.LogError("Terrain or prefabs not assigned.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = terrain.transform.position;

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Generate a random position within the terrain bounds
            float randomX = Random.Range(0, terrainData.size.x);
            float randomZ = Random.Range(0, terrainData.size.z);

            // Get the height of the terrain at the random position
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrainPosition.y;

            // Create a spawn position
            Vector3 spawnPosition = new Vector3(randomX + terrainPosition.x, terrainHeight, randomZ + terrainPosition.z);

            // Check if the position is valid (e.g., avoid overlapping if necessary)
            if (IsValidPosition(spawnPosition))
            {
                // Select a random prefab
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                // Instantiate the prefab
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// Validates the position for prefab placement (e.g., spacing).
    /// </summary>
    private bool IsValidPosition(Vector3 position)
    {
        // Add logic to check if the position is valid, such as ensuring spacing.
        // For simplicity, this example assumes all positions are valid.
        // You can add logic to track and check distances between placed objects.
        return true;
    }
}
