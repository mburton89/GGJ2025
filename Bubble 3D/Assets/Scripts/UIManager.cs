using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore;

    public Image boostFill;
    public float currentAirAmount;
    public float maxAirAmount = 100;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        currentAirAmount = maxAirAmount;
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.B))
        {
            AdjustSlider(10);
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            AdjustSlider(-10);
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        scoreText.text = "Score: " + currentScore;
    }

    public void AdjustSlider(float amountOfAirToAdd)
    {
        currentAirAmount += amountOfAirToAdd;
        boostFill.fillAmount = currentAirAmount / maxAirAmount;
    }

}
