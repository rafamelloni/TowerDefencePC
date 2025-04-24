using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPanels : MonoBehaviour
{
    public GameObject objectToActivate; // El GameObject que este botón activa
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

        // Activa el GameObject de este botón
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
