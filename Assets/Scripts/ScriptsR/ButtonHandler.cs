using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button myButton; // Arrastra el botón aquí desde el Inspector
    public GameObject torrePreFab;
    public GameObject spawner;

    void Start()
    {
        // Asegúrate de que el botón esté asignado
        if (myButton != null)
        {
            // Agrega el método que deseas llamar cuando se hace clic en el botón
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        // Aquí va la lógica que deseas ejecutar cuando se hace clic en el botón
        Debug.Log("Button clicked!");
        Instantiate(torrePreFab, spawner.transform.position, Quaternion.identity);
    }
}
