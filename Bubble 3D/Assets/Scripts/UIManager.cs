using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore;

    public GameObject startButton;
    public GameObject quitButton;


    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(1);
        //print("MainMenu Loaded");
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        currentScore += points;
        scoreText.text = "Score: " + currentScore;
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void OnQuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
