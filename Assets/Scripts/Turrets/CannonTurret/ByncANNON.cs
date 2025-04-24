using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ByncANNON : MonoBehaviour
{
    public CannonUpgradeManager turretManager;
    public Button btn;

    public float buttonCooldown = 0.5f;
    private bool isButtonCooldown = false;



    private void Start()
    {
        turretManager = FindObjectOfType<CannonUpgradeManager>();

        // Asignar la función al botón

        btn.onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        print("botonclick" + isButtonCooldown);
        if (!isButtonCooldown)
        {
            Debug.Log("<color=green>BotonPrimero</color>");
            turretManager.OnUpgradeButtonPressed();
            StartCoroutine(ButtonCooldown());
        }
    }

    private IEnumerator ButtonCooldown()
    {
        isButtonCooldown = true;
        btn.interactable = false;  // Deshabilitar el botón mientras está en enfriamiento

        // Esperar el tiempo de cooldown
        yield return new WaitForSeconds(buttonCooldown);

        btn.interactable = true;  // Volver a habilitar el botón
        isButtonCooldown = false;  // Terminar el enfriamiento
    }
}
