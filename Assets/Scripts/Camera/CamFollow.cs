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
    public Button shopButton; // Bot�n para acceder a la tienda

    public CinemachineVirtualCamera mainCamera;  // C�mara principal (Cinemachine)

    private Vector3 initialPos;
    private Quaternion initialRotation;

    // Define los l�mites de la c�mara
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private void Awake()
    {
        shopButton.gameObject.SetActive(false);  // Desactiva el bot�n de la tienda al inicio
        initialPos = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula la nueva posici�n de la c�mara para la c�mara principal
            Vector3 newPosition = new Vector3(player.position.x, transform.position.y, player.position.z + offsetZ);

            // Clampeamos la posici�n en el �rea rectangular
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            transform.position = newPosition;
        }
    }

    private void Update()
    {
        // Mostrar el bot�n de la tienda solo si el jugador est� cerca
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
