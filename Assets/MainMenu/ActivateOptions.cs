using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas optionsCanvas;
   public void  OnButtonClick ()
    {
        optionsCanvas.gameObject.SetActive(true);
    }
    public void OnButtonClickReturn()
    {
        optionsCanvas.gameObject.SetActive(false);
    }
}
