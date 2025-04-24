using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausaBoton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button pauseButton;  // El botón que controla el canvas de pausa

    void Start()
    {
        // Asegurarse de que el canvas de pausa esté correctamente configurado
        if (PauseMenu.instance != null)
        {
            // Asignar el evento OnClick del botón para que active o desactive el canvas
            pauseButton.onClick.AddListener(PauseMenu.instance.OnButtonPress);
        }
    }
}
