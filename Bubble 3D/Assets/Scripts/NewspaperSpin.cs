using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewspaperSpin : MonoBehaviour
{
    public RawImage newspaper;
    public List<Texture2D> badsprites;
    public List<Texture2D> goodsprites;
    public UIManager uiManager;
    public RectTransform rectTransform;
    public int timer = 0;

    public void Start()
    {
        timer = 0;
        rectTransform = GetComponent<RectTransform>();
        uiManager = FindObjectOfType<UIManager>();
        print(uiManager.finalScore);
        if (uiManager.finalScore < 500)
        {
            int i_ChosenNewspaper = Random.Range(0, 4);
            print(badsprites[i_ChosenNewspaper].name);
            newspaper.texture = badsprites[i_ChosenNewspaper];
        } else
        {
            int i_ChosenNewspaper = Random.Range(0, 4);
            print(goodsprites[i_ChosenNewspaper].name);
            newspaper.texture = goodsprites[i_ChosenNewspaper];
        }
        
    }

    public void Update()
    {
        
        if (timer < 500)
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta * 1.006f;
            rectTransform.Rotate(new Vector3(0, 0, 1), 360 * Time.deltaTime);
            timer++;
        }
    }

}
