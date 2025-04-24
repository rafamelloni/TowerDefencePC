using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPanels : MonoBehaviour
{
    public GameObject objectToActivate; // El GameObject que este bot�n activa
    public ButtonsPanels[] otherButtons; // Otros botones que deben desactivar sus GameObjects

    public void OnButtonClick()
    {
        // Desactiva otros GameObjects
        foreach (var button in otherButtons)
        {
            if (button.objectToActivate != null)
            {
                button.objectToActivate.SetActive(false);
            }
        }

        // Activa el GameObject de este bot�n
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
