using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private TurretUpgradeManager turretManager;
    public Button btn;

    // Tiempo de enfriamiento entre clics
    public float buttonCooldown = 0.5f;
    private bool isButtonCooldown = false;

    private void Start()
    {
        turretManager = FindObjectOfType<TurretUpgradeManager>();

        // Asignar la función al botón
        btn.onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        // Si el botón no está en enfriamiento, proceder con la mejora
        if (!isButtonCooldown)
        {
            turretManager.OnUpgradeButtonPressed();
            StartCoroutine(ButtonCooldown());  // Iniciar el enfriamiento
        }
    }

    // Coroutine para gestionar el enfriamiento del botón
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
