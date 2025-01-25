using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public GameObject playerPrefab; // Player prefab with PlayerController
    public Transform player1Spawn;
    public Transform player2Spawn;

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    //private void OnDisable()
    //{
    //    PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    //}

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if (FindObjectOfType<StartingCamera>())
        {
            Destroy(FindObjectOfType<StartingCamera>().gameObject);
        }

        // Assign spawn points dynamically
        if (PlayerInput.all.Count == 1)
        {
            playerInput.transform.position = player1Spawn.position;
        }
        else if (PlayerInput.all.Count == 2)
        {
            playerInput.transform.position = player2Spawn.position;
        }

        //if (FindObjectsOfType<AudioListener>().Length > 1)
        //{
        //    Destroy(FindObjectOfType<AudioListener>().gameObject);
        //}
    }
}
