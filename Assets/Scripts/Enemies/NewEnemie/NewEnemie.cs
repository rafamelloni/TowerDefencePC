using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemie : MonoBehaviour
{
    // Referencia al jugador
    public Transform _player;

    // Lista de torretas
    private List<Turrets> turrets = new List<Turrets>();

    // Velocidad de movimiento
    private float speed = 3f;

    // El objetivo actual (jugador o torreta)
    private Transform currentTarget;

    // Radio de la zona "personal" del enemigo
    public float personalSpaceRadius = 3f;

   public  static bool isAttack;

    private void UpdateTurretList()
    {
        // Encuentra todas las torretas activas en la escena
        Turrets[] detectedTurrets = FindObjectsOfType<Turrets>();

        // Agrega las torretas nuevas que no estén ya en la lista
        foreach (var turret in detectedTurrets)
        {
            if (!turrets.Contains(turret))
            {
                turrets.Add(turret);
            }
        }
        
    }

    private void MoveTowardsTarget()
    {
        if (currentTarget == null) return;

        // Calcular la dirección hacia el objetivo
        Vector3 direction = (currentTarget.position - transform.position).normalized;

        // Calcular la distancia al objetivo
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        // Distancia mínima a la torreta para detenerse
        float stopDistance = 2f; // Cambia este valor según lo necesites

        // Solo aplicar la frenada si el objetivo es una torreta
        if (currentTarget != _player && distanceToTarget < stopDistance)
        {
            // Detenerse si estamos cerca de la torreta
            speed = 0f;
            
        }
        else
        {
            // Restaurar la velocidad cuando estamos lejos de la torreta o el objetivo es el jugador
            speed = 3f;
            
        }

        // Mover al enemigo hacia el objetivo, congelando el movimiento en el eje Y
        Vector3 movement = direction * speed * Time.deltaTime;
        movement.y = 0;  // Esto asegura que no haya movimiento en el eje Y

        transform.Translate(movement, Space.World);

        // Rotar hacia el objetivo
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void UpdateCurrentTarget()
    {
        if (turrets.Count == 0)
        {
            // Si no hay torretas, siempre sigue al jugador
            currentTarget = _player;
        }
        else
        {
            // Encuentra la torreta más cercana
            Turrets closestTurret = null;
            float closestDistance = Mathf.Infinity;

            foreach (var turret in turrets)
            {
                if (turret == null || turret.gameObject == null) continue;

                float distance = Vector3.Distance(turret.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTurret = turret;
                }
            }

            // Decide si seguir al jugador o a la torreta más cercana
            float playerDistance = Vector3.Distance(_player.position, transform.position);

            if (playerDistance < 3 || closestTurret == null)
            {
                // Si el jugador está suficientemente cerca, priorízalo
                currentTarget = _player;
            }
            else
            {
                // Si hay una torreta más cercana y el jugador no está cerca, sigue la torreta
                currentTarget = closestTurret.transform;
            }
        }
    }

    // Método para evitar que los enemigos se apilen
    private void AvoidCollisionsWithOtherEnemies()
    {
        // Buscar otros enemigos
        NewEnemie[] enemies = FindObjectsOfType<NewEnemie>();

        foreach (var enemy in enemies)
        {
            // Evitar que el enemigo se apile con otros (no puede ser él mismo)
            if (enemy != this)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < personalSpaceRadius)
                {
                    // Si está demasiado cerca de otro enemigo, moverlo hacia una dirección diferente
                    Vector3 directionAwayFromEnemy = (transform.position - enemy.transform.position).normalized;
                    directionAwayFromEnemy.y = 0;  // Asegura que no haya movimiento en Y
                    transform.Translate(directionAwayFromEnemy * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }

    // Llamamos a la función para evitar que se apilen
    private void LateUpdate()
    {
        AvoidCollisionsWithOtherEnemies();
    }

    // Método para dibujar el Gizmo
    private void OnDrawGizmos()
    {
        // Dibujar una esfera para mostrar el "espacio personal" del enemigo
        Gizmos.color = Color.red;  // Color del gizmo (puedes cambiarlo)
        Gizmos.DrawWireSphere(transform.position, personalSpaceRadius);
    }

    private void Update()
    {
        UpdateTurretList();
        UpdateCurrentTarget();
        MoveTowardsTarget();
    }
}
