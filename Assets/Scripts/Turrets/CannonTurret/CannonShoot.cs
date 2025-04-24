using System.Collections;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [Header("Torreta Configuración")]
    public float range;          // Rango de detección
    public float fireRate;        // Tiempo entre disparos
    public float rotationSpeed = 5f;   // Velocidad de giro
    public GameObject aoeBulletPrefab; // Prefab del proyectil AOE
    public Transform firePoint;        // Punto de disparo

    public int clickCount;
    private int maxUpgradeLevel = 3; // Nivel máximo de mejora

    private Transform target;
    private float fireCountdown = 0f;
    public AudioClip fireSoundClip;

    void Update()
    {
        print("firerate " + fireRate);
        BuscarEnemigo();

        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }

        if (target != null)
        {
            ApuntarAlEnemigo();
            if (fireCountdown <= 0f)
            {
                Disparar();
                fireCountdown = fireRate;  // Reinicia el tiempo de espera tras disparar
            }
        }
    }

    void BuscarEnemigo()
    {
        Collider[] enemigos = Physics.OverlapSphere(transform.position, range);
        float menorDistancia = Mathf.Infinity;
        Transform enemigoMasCercano = null;

        foreach (Collider enemigo in enemigos)
        {
            if (!enemigo.CompareTag("Enemy") && !enemigo.CompareTag("FinalBoss")) continue;
            // Filtrar solo los enemigos

            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                enemigoMasCercano = enemigo.transform;
            }
        }

        target = enemigoMasCercano;
    }

    void ApuntarAlEnemigo()
    {
        if (target == null) return;

        // Obtener la dirección sin la altura (mantener Y en la misma posición)
        Vector3 dir = target.position - transform.position;
        dir.y = 0; // Ignorar la diferencia en altura

        // Rotar suavemente hacia el enemigo
        Quaternion rotacion = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * rotationSpeed);
    }

    void Disparar()
    {
        GameObject bala = Instantiate(aoeBulletPrefab, firePoint.position, firePoint.rotation);
        SoundFXManager.instance.PlaySoundFXClip(fireSoundClip, transform, 0.5f);
        AreaBullet bullet = bala.GetComponent<AreaBullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }


        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Upgrade()
    {
        print("<color/red>Upgrade<color>");
        if (clickCount >= maxUpgradeLevel)
        {
            // Si ya ha llegado al máximo nivel, no hacer nada más
            Debug.Log("Esta torreta ya está al máximo nivel.");
            return;
        }




        // Se pueden agregar más mejoras según el contador de clics
        if (clickCount == 0 && GameManager.Instance.coins >= 50)
        {
            GameManager.Instance.DecreaseCoins(50);
            range = 15f;
            
            clickCount++;
        }
        else if (clickCount == 1 && GameManager.Instance.coins >= 100)
        {
            fireRate = 1.75f;
            GameManager.Instance.DecreaseCoins(100);
            
            clickCount++;
            
        }
        else if (clickCount == 2 && GameManager.Instance.coins >= 150)
        {
            GameManager.Instance.DecreaseCoins(150);
            fireRate = 1.5f;
            clickCount++;
            
        }
        else
        {
            GameManager.Instance.InsufficientFunds();
        }
    }
}
