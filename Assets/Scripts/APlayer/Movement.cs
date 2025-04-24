using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    public float rotationSpeed = 700f; // Velocidad de rotación del personaje
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual de Cinemachine

    private Camera mainCamera;
    public Animator anim;
    private void Start()
    {
        // Obtener la CinemachineBrain (es el que controla la cámara activa)
        CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        if (cinemachineBrain != null)
        {
            // Obtener la cámara activa que está controlando la CinemachineBrain
            mainCamera = cinemachineBrain.GetComponent<Camera>();

            if (mainCamera == null)
            {
                Debug.LogError("No se encontró la cámara vinculada al CinemachineBrain.");
            }
        }
        else
        {
            Debug.LogError("No se encontró un CinemachineBrain en la escena.");
        }
    }

    private void Update()
    {
        MoveCharacter(); // Mover el personaje
       RotateCharacter(); // Rotar el personaje hacia el ratón

        //playerclamp
        float minX = 363f, maxX = 438f;
        float minZ = 145f, maxZ = 200f;

        // Obtener la posición actual del personaje
        Vector3 pos = transform.position;

        // Limitar la posición dentro de los rangos permitidos
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        // Aplicar la posición restringida
        transform.position = pos;
    }

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Dirección de movimiento en el mundo
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Si hay movimiento
        if (moveDirection.magnitude > 0.1f)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(moveDirection * step, Space.World);
        }

        // Convertir dirección a espacio local (relativo al personaje)
        Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection);

        // Asignar valores al Animator
        anim.SetFloat("Horizontal", localMoveDirection.z); //z
        anim.SetFloat("Vertical", localMoveDirection.x); //x
    }



    private void RotateCharacter()
    {
        if (mainCamera == null)
        {
            Debug.Log("No hay cámara.");
            return;
        }

        // Obtener la posición del ratón en el mundo 3D
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Si el rayo toca algo, rotamos el personaje hacia el punto de colisión
            Vector3 direction = hit.point - transform.position;
            direction.y = 0; // Asegurarse de que el personaje solo rota en el eje Y

            // Si la dirección es válida, rotamos el personaje suavemente
            if (direction.magnitude > 0.1f)
            {
                // Calcular la rotación
                Quaternion toRotation = Quaternion.LookRotation(direction);

                // Rotación más suave y controlada con Slerp
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    
        

}
