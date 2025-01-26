using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int deliveryCount;
    public int trickCount;
    public int scoreCount;

    public TextMeshProUGUI deliveryCountText;
    public TextMeshProUGUI trickCountText;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverContainer;
    public Button restartButton;
    public Button mainMenuButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public void HandleGameOver()
    {
        gameOverContainer.SetActive(true);
        restartButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        deliveryCountText.SetText("You delivered " + deliveryCount + " newspapers!");
        trickCountText.SetText("You pulled off " + trickCount + " tricks!");
        scoreText.SetText("Total Score: " + FindObjectOfType<UIManager>().currentScore);

        Time.timeScale = 0;
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
