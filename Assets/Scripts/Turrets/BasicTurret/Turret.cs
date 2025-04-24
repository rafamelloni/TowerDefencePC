using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Turret : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    public TurretSO normalTurretData;
    public Vector3 offset = new Vector3(0, 0, 180);

    private float nextFireTime = 0f;
    public float fireRate;
    public float rotSpeed;
    public float dist;
    public int clickCount;
    private int maxUpgradeLevel = 3; // Nivel máximo de mejora


    public TMP_Text textPrecioDeMejora;

    public int kills;


    

    private void Start()
    {
        fireRate = normalTurretData.fireRate;
        rotSpeed = normalTurretData.rotationSpeed;
        dist = normalTurretData.dist;
    }

    public void Upgrade()
    {
        if (clickCount >= maxUpgradeLevel)
        {
            return;
        }

        // Verifica si hay suficientes monedas ANTES de modificar `clickCount`
        if (clickCount == 0 && GameManager.Instance.coins >= 50)
        {
            GameManager.Instance.DecreaseCoins(50);
            fireRate = 0.2f;
            clickCount++;
        }
        else if (clickCount == 1 && GameManager.Instance.coins >= 70)
        {
            GameManager.Instance.DecreaseCoins(70);
            rotSpeed = 10;
            clickCount++;
        }
        else if (clickCount == 2 && GameManager.Instance.coins >= 90)
        {
            GameManager.Instance.DecreaseCoins(90);
            fireRate = 0.17f;
            dist = 25;
            clickCount++;
        }
        else
        {

        GameManager.Instance.InsufficientFunds();
        }
    }




    private void Update()
    {
      
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // Encontrar el enemigo más cercano
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemyObjects)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        // Si hay un enemigo válido
        if (closestEnemy != null && closestDistance < normalTurretData.rotationDistance)
        {
            Transform enemyTransform = closestEnemy.transform;

            // Calcular la dirección hacia el enemigo (ignorando la diferencia en Y)
            Vector3 direction = enemyTransform.position - transform.position;
            direction.y = 0; // Asegurar que la rotación ocurre solo en el plano horizontal

            // Evitar rotaciones innecesarias
            if (direction.magnitude > 0.1f)
            {
                // Calcular la rotación hacia el enemigo
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Aplicar un offset de 180° en el eje Y
                Quaternion offsetRotation = Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * offsetRotation, Time.deltaTime * normalTurretData.rotationSpeed);
            }

            // Si el enemigo está dentro del rango de disparo, dispara
            if (closestDistance < normalTurretData.dist && Time.time >= nextFireTime)
            {
                _weapon.Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }

     
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // Color de la esfera del Gizmo
        Gizmos.DrawWireSphere(transform.position, 2f);  // Dibuja la esfera de colisión con radio de 2 unidades
    }



}



