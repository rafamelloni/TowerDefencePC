using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class HealtBarTurret : MonoBehaviour
{
    
    private Camera cam;
    public Slider healthSlider;  



    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        // Calcula el porcentaje de salud restante
        if (healthSlider == null)
        {
            Debug.LogError("El Slider de la barra de salud no est� asignado en " + gameObject.name);
            return;
        }

        healthSlider.maxValue = maxHealth;  // Asegurar que el m�ximo sea correcto
        healthSlider.value = currentHealth; // Actualizar la vida actual
    }

    private void Start()
    {
        cam = Camera.main;  // Asignar la c�mara principal
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);  // Mantener la barra mirando a la c�mara
    }
}
    