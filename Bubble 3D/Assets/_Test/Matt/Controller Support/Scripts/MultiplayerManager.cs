using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public GameObject playerPrefab; // Player prefab with PlayerController
    public Transform player1Spawn;
    public Transform player2Spawn;

    private bool player1Spawned = false;

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;

        //if (!player1Spawned)
        //{
        //    PlayerInputManager.instance.JoinPlayer();
        //    player1Spawned = true;
        //}
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if (FindObjectOfType<StartingCamera>())
        {
            Destroy(FindObjectOfType<StartingCamera>().gameObject);
        }


        var characterController = playerInput.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false; // Disable to avoid conflicts
        }

        if (PlayerInput.all.Count == 1)
        {
            playerInput.transform.position = player1Spawn.position;
        }
        else if (PlayerInput.all.Count == 2)
        {
            playerInput.transform.position = player2Spawn.position;
        }

        if (characterController != null)
        {
            characterController.enabled = true; // Re-enable after setting position
        }
    }
}
