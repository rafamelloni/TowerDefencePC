using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _lastMousePos;
    public Camera cam;
    public float cameraSpeed = 0.1f; // Ajusta esta velocidad seg�n lo necesites

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main; // Asigna la c�mara principal si no se ha asignado una c�mara
        }

        // Inicializa _lastMousePos con la posici�n actual del mouse en el mundo
        _lastMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void cameraMove(Vector3 inputPosition)
    {
        // Convierte la posici�n de entrada de pantalla a coordenadas del mundo
        Vector3 newMousePos = cam.ScreenToWorldPoint(inputPosition);

        // Calcula la diferencia entre la posici�n actual y la nueva posici�n del mouse
        Vector3 delta = newMousePos - _lastMousePos;

        // Calcula el movimiento basado en la velocidad de la c�mara
        Vector3 move = new Vector3(delta.x * cameraSpeed, 0, delta.z * cameraSpeed);

        // Aplica el movimiento a la c�mara
        cam.transform.Translate(move, Space.World);

        // Actualiza la �ltima posici�n del mouse
        _lastMousePos = newMousePos;
    }
}

