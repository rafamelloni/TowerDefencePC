using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRayCast : MonoBehaviour
{

    // Longitud del raycast
    public float rayLength = 0.5f;
    public bool puedePosicionarseAca = false;

    void Update()
    {
        // Posición del cubo (desde su parte inferior)
        Vector3 rayOrigin = transform.position + Vector3.down * (transform.localScale.y / 2);

        // Dirección hacia abajo
        Vector3 rayDirection = Vector3.down;

        // Dibujar el raycast en la escena para poder visualizarlo
        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

        // Lanzar el raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayLength))
        {
            // Imprimir el nombre del objeto con el que colisiona
            
            if (hit.collider.gameObject.CompareTag("Cube"))
            {
                puedePosicionarseAca = true;
            }
            else
            {
                puedePosicionarseAca = false;
            }
        }
    }
}


