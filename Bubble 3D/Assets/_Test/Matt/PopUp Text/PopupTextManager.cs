using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextManager : MonoBehaviour
{
    public static PopupTextManager instance;
    public GameObject popupTextPrefab;
  
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPopupText(Transform textPosition, string textToShow)
    { 
        GameObject popupText = Instantiate(popupTextPrefab, textPosition.position, Quaternion.identity, null);
        popupText.GetComponent<ComicText>().Init(textToShow, textPosition);
    }
}
