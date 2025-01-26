using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public GameObject playerPrefab; // Player prefab with PlayerController
    public Transform player1Spawn;
    public Transform player2Spawn;

    private bool player1Spawned = false;

    GameObject firstJoinedPlayer;

    public GameObject title;

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        title.SetActive(false);
        GameTimer.Instance.timerRunning = true;

        if (!player1Spawned)
        {
            player1Spawned = true;
            firstJoinedPlayer = playerInput.gameObject;
        }

        var characterController = playerInput.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false; // Disable to avoid conflicts
        }

        var rigidbody = playerInput.GetComponentInChildren<Rigidbody>();
        if (rigidbody != null)
        {
            print("Found RigidBody on " + rigidbody.gameObject.name);
            rigidbody.isKinematic = true; // Temporarily disable physics to set position
        }

        if (PlayerInput.all.Count == 1)
        {
            if (rigidbody != null)
            {
                rigidbody.position = player1Spawn.position; // Directly set the position
            }
            else
            { 
                playerInput.transform.position = player1Spawn.position;
            }
        }
        else if (PlayerInput.all.Count == 2)
        {
            if (rigidbody != null)
            {
                rigidbody.position = player2Spawn.position; // Directly set the position
            }
            else
            {
                playerInput.transform.position = player2Spawn.position;
            }
        }
        else if (PlayerInput.all.Count == 3)
        {
            Destroy(firstJoinedPlayer);
            if (rigidbody != null)
            {
                rigidbody.position = player1Spawn.position; // Directly set the position
            }
            else
            {
                playerInput.transform.position = player1Spawn.position;
            }
        }

        if (characterController != null)
        {
            characterController.enabled = true; // Re-enable after setting position
        }

        if (rigidbody != null)
        {
            rigidbody.isKinematic = false; // Re-enable physics
        }
    }
}
