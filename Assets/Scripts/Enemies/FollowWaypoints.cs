using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    public Transform[] waypoints; // Lista de puntos de destino
    private int currentWaypointIndex = 0;
    public float speed ;
    private bool isSlowed = false;
    private float totalSlowAmount = 0f;
    public float minSpeed = 3f;
    private float originalSpeed;

    public float rotationSpeed = 5f; // Velocidad de rotación (ajustable)

    //private void Awake()
    //{
    //    speed = 2f;
    //}

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        originalSpeed = speed;
    }

    void Update()
    {
        if (waypoints.Length == 0)
            return;
      
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;

        // Rotar suavemente hacia el waypoint
        RotateTowards(targetWaypoint.position);

        // Movimiento suave
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Comprueba si ha llegado al waypoint actual
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Reinicia el ciclo
            }
        }
    }

    // Método para rotar suavemente hacia el punto de destino
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection.y = 0; // Para que solo gire en el plano horizontal

        if (targetDirection.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void ResetWaypoints()
    {
        currentWaypointIndex = 0;
    }

    public void ApplySlow(float slowAmmount)
    {
        totalSlowAmount += slowAmmount;  // Acumula el efecto de la lentitud
        speed = Mathf.Max(originalSpeed - totalSlowAmount, minSpeed);  // Asegura que la velocidad no caiga por debajo del mínimo
    }

    public void RemoveSlow(float slowAmmount)
    {
        totalSlowAmount -= slowAmmount;  // Resta la lentitud aplicada por esta torreta
        if (totalSlowAmount < 0) totalSlowAmount = 0;  // No permitir valores negativos
        speed = Mathf.Max(originalSpeed - totalSlowAmount, minSpeed);  // Asegura que la velocidad no caiga por debajo del mínimo
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }
}
