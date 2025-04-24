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

        // Asignar la funci�n al bot�n
        btn.onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        // Si el bot�n no est� en enfriamiento, proceder con la mejora
        if (!isButtonCooldown)
        {
            turretManager.OnUpgradeButtonPressed();
            StartCoroutine(ButtonCooldown());  // Iniciar el enfriamiento
        }
    }

    // Coroutine para gestionar el enfriamiento del bot�n
    private IEnumerator ButtonCooldown()
    {
        isButtonCooldown = true;
        btn.interactable = false;  // Deshabilitar el bot�n mientras est� en enfriamiento

        // Esperar el tiempo de cooldown
        yield return new WaitForSeconds(buttonCooldown);

        btn.interactable = true;  // Volver a habilitar el bot�n
        isButtonCooldown = false;  // Terminar el enfriamiento
    }
}
