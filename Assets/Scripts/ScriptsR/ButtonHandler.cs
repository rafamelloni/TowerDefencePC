using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button myButton; // Arrastra el bot�n aqu� desde el Inspector
    public GameObject torrePreFab;
    public GameObject spawner;

    void Start()
    {
        // Aseg�rate de que el bot�n est� asignado
        if (myButton != null)
        {
            // Agrega el m�todo que deseas llamar cuando se hace clic en el bot�n
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        // Aqu� va la l�gica que deseas ejecutar cuando se hace clic en el bot�n
        Debug.Log("Button clicked!");
        Instantiate(torrePreFab, spawner.transform.position, Quaternion.identity);
    }
}
