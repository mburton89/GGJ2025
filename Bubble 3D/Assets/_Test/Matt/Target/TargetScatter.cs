using UnityEngine;

public class TargetScatter : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject[] prefabs; // Array of prefabs to scatter
    public Terrain terrain; // Reference to the terrain
    public int numberOfPrefabs = 40; // Number of prefabs to scatter

    [Header("Placement Settings")]
    public float minSpacing = 10f; // Minimum spacing between prefabs
    public float prefabHeightOffset = 0f; // Adjust for prefabs that don't rest at the base

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

            // Validate position and adjust for prefab height offset
            spawnPosition.y += prefabHeightOffset;

            if (IsValidPosition(spawnPosition))
            {
                // Select a random prefab
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                // Instantiate the prefab
                GameObject instantiatedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);

                // Center prefab vertically (using renderer bounds)
                AlignToTerrain(instantiatedPrefab, spawnPosition);
            }
        }
    }

    /// <summary>
    /// Aligns the prefab based on its Renderer bounds to ensure it rests on the terrain.
    /// </summary>
    private void AlignToTerrain(GameObject prefab, Vector3 position)
    {
        Renderer renderer = prefab.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            float prefabBaseHeight = renderer.bounds.min.y - prefab.transform.position.y;
            prefab.transform.position = new Vector3(position.x, position.y - prefabBaseHeight, position.z);
        }
    }

    /// <summary>
    /// Validates the position for prefab placement (e.g., spacing).
    /// </summary>
    private bool IsValidPosition(Vector3 position)
    {
        // Example: Add spacing validation here if needed
        return true;
    }
}
