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

    [HideInInspector] public bool gameIsOver;

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
        Newspaper[] newspapers = FindObjectsByType<Newspaper>(FindObjectsSortMode.None);
        foreach (Newspaper newspaper in newspapers)
        {
            Destroy(newspaper.gameObject);
        }
        gameIsOver = true;

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

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
