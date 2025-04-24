using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _lastMousePos;
    public Camera cam;
    public float cameraSpeed = 0.1f; // Ajusta esta velocidad según lo necesites

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main; // Asigna la cámara principal si no se ha asignado una cámara
        }

        // Inicializa _lastMousePos con la posición actual del mouse en el mundo
        _lastMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void cameraMove(Vector3 inputPosition)
    {
        // Convierte la posición de entrada de pantalla a coordenadas del mundo
        Vector3 newMousePos = cam.ScreenToWorldPoint(inputPosition);

        // Calcula la diferencia entre la posición actual y la nueva posición del mouse
        Vector3 delta = newMousePos - _lastMousePos;

        // Calcula el movimiento basado en la velocidad de la cámara
        Vector3 move = new Vector3(delta.x * cameraSpeed, 0, delta.z * cameraSpeed);

        // Aplica el movimiento a la cámara
        cam.transform.Translate(move, Space.World);

        // Actualiza la última posición del mouse
        _lastMousePos = newMousePos;
    }
}

