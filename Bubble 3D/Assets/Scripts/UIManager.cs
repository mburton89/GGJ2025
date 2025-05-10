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
    public int maxSlider;
    public int currentSlider;


    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AdjustSlider(0.1f);
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            AdjustSlider(-0.1f);
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        scoreText.text = "Score: " + currentScore;
    }

    public void AdjustSlider(float amountToAdd)
    {
        boostFill.fillAmount += amountToAdd;

    }

}
