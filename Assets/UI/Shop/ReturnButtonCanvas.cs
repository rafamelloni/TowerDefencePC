using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonCanvas : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas shopCanvas;
    public CamFollow cam;


    public void OnReturnButton()
    {
        
        shopCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        
    }
}
