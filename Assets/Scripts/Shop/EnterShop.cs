using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    
    public Canvas shopMenu;
    public Canvas mainCanvas;

    public bool enteredShop = false;



    public void OnButtonShop()
    {
        enteredShop = true;
        gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);



    }
}
