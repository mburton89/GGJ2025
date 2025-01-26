using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    [Header("Timer Settings")]
    public float startTime = 120f; // Starting time in seconds
    public TextMeshProUGUI timerText; // UI Text component to display the timer
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;

    private float timeRemaining;
    private bool timerRunning;

    public float timeToAdd = 2f;

    private void Awake()
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

    void Start()
    {
        timeRemaining = startTime;
        timerRunning = true;
        UpdateTimerUI();


    }

    void Update()
    {
        if (timerRunning)
        {
            // Decrease time
            timeRemaining -= Time.deltaTime;

            // Clamp the time to ensure it doesn't go below zero
            timeRemaining = Mathf.Max(timeRemaining, 0);

            // Update the timer UI
            UpdateTimerUI();

            // Stop the timer if it reaches zero
            if (timeRemaining <= 0)
            {
                timerRunning = false;
                OnTimerEnd();
            }
        }
    }

    /// <summary>
    /// Updates the timer UI text and color based on the remaining time.
    /// </summary>
    private void UpdateTimerUI()
    {
        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        // Change text color if time is below the warning threshold
        if (timeRemaining < 15f)
        {
            timerText.color = warningColor;
        }
        else
        {
            timerText.color = normalColor;
        }
    }

    /// <summary>
    /// Increases the time remaining by a specified amount.
    /// </summary>
    /// <param name="timeToAdd">Time to add in seconds.</param>
    public void AddTime()
    {
        timeRemaining += timeToAdd;

        // Ensure the timer runs if it was stopped
        if (!timerRunning && timeRemaining > 0)
        {
            timerRunning = true;
        }

        UpdateTimerUI();
    }

    /// <summary>
    /// Called when the timer reaches zero.
    /// </summary>
    private void OnTimerEnd()
    {
        // Add logic here for what happens when the timer reaches zero
        Debug.Log("Time's up!");
        GameManager.Instance.HandleGameOver();
    }
}
