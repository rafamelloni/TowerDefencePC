using System.Collections.Generic;
using UnityEngine;

public class RobotTargeting : MonoBehaviour
{
    public float detectionRadius = 5f; // Radio de detección
    private List<Transform> enemiesInRange = new List<Transform>(); // Lista de enemigos detectados
    private Transform currentTarget; // Enemigo más cercano

    public RobotShooting shoot;
    public bool hasEnemiesInRange = false;

    void Update()
    {
        // Verificar a los enemigos dentro del rango cada frame
        CheckEnemiesInRange();
        UpdateTarget();
        
    }

    void CheckEnemiesInRange()
    {
        // Buscar todos los enemigos en la escena con el tag "Enemy2"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Recorrer todos los enemigos en la escena
        for (int i = enemiesInRange.Count - 1; i >= 0; i--) // Recorremos la lista al revés para eliminar de forma segura
        {
            Transform enemy = enemiesInRange[i];
            float distance = Vector3.Distance(transform.position, enemy.position);

            if (distance > detectionRadius)
            {
                // Si el enemigo se sale del radio, eliminarlo de la lista
                enemiesInRange.RemoveAt(i);
            }
        }

        // Ahora agregar nuevos enemigos dentro del radio
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius && !enemiesInRange.Contains(enemy.transform))
            {
                // Agregar el enemigo a la lista si no está ya en ella
                enemiesInRange.Add(enemy.transform);
            }
        }

        hasEnemiesInRange = enemiesInRange.Count > 0;

        // Imprimir la cantidad de enemigos detectados
    }

    void UpdateTarget()
    {
        if (enemiesInRange.Count == 0)
        {
            currentTarget = null;
            return;
        }

        // Buscar el enemigo más cercano
        float minDistanceSqr = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Transform enemy in enemiesInRange)
        {
            float distanceSqr = (transform.position - enemy.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
        transform.LookAt(currentTarget);
        shoot.Shoot();

    }

    // Usar Gizmos para visualizar la zona de detección
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    

 
}
