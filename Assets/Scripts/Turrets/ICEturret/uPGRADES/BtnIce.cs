using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnIce : MonoBehaviour
{
    private IceTurretUpgrade turretManager;
    public Button btn;

    public float buttonCooldown = 0.5f;
    private bool isButtonCooldown = false;



    private void Start()
    {
        turretManager = FindObjectOfType<IceTurretUpgrade>();

        // Asignar la función al botón

        btn.onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        if (!isButtonCooldown)
        {

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
