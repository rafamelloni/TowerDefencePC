using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CamFollow : MonoBehaviour
{
    public Transform player; // Asigna el objeto jugador en el inspector
    public float offsetZ = -10f; // Desplazamiento en el eje Z
    public Transform shop;  // La tienda
    public Button shopButton; // Botón para acceder a la tienda

    public CinemachineVirtualCamera mainCamera;  // Cámara principal (Cinemachine)

    private Vector3 initialPos;
    private Quaternion initialRotation;

    // Define los límites de la cámara
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private void Awake()
    {
        shopButton.gameObject.SetActive(false);  // Desactiva el botón de la tienda al inicio
        initialPos = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula la nueva posición de la cámara para la cámara principal
            Vector3 newPosition = new Vector3(player.position.x, transform.position.y, player.position.z + offsetZ);

            // Clampeamos la posición en el área rectangular
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            transform.position = newPosition;
        }
    }

    private void Update()
    {
        // Mostrar el botón de la tienda solo si el jugador está cerca
        if (Vector3.Distance(player.position, shop.position) < 10f)
        {
            shopButton.gameObject.SetActive(true);
        }
        else
        {
            shopButton.gameObject.SetActive(false);
        }
    }
}
