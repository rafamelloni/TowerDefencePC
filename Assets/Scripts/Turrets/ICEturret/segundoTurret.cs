using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class segundoTurret : Turrets
{
    public float radius;
    public float slowAmmount;

    private List<FollowWaypoints> slowedEnemies = new List<FollowWaypoints>();
    public int clickCount;
    private int maxUpgradeLevel = 3; // Nivel máximo de mejora

    public ParticleSystem particles;

    [SerializeField] AudioClip electricitySoundClip;


    //vida
    public static event Action<segundoTurret> OnTurretDestroyed;
    
    //public PlaceablePlatform platform;


    // Start is called before the first frame update
    public override void Die()
    {
        OnTurretDestroyed?.Invoke(this);
        platform.Vacate(this);
        Destroy(gameObject);
    }

    void Start()
    {
        type = TurretType.SlowTurret;
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        List<FollowWaypoints> currentlyInRange = new List<FollowWaypoints>();

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                FollowWaypoints enemy = hit.GetComponent<FollowWaypoints>();
                if (enemy != null)
                {
                    currentlyInRange.Add(enemy);
                    if (!slowedEnemies.Contains(enemy))
                    {
                        SoundFXManager.instance.PlaySoundFXClip(electricitySoundClip, transform, 0.5f);
                        enemy.ApplySlow(slowAmmount);
                        slowedEnemies.Add(enemy);
                    }
                }
            }
        }

        // Revisar si algún enemigo salió del radio
        for (int i = slowedEnemies.Count - 1; i >= 0; i--)
        {
            if (!currentlyInRange.Contains(slowedEnemies[i]))
            {
                slowedEnemies[i].RemoveSlow(slowAmmount);
                slowedEnemies.RemoveAt(i);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

 
    public void Upgrade()
    {
        var main = particles.main;
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
            radius += 3.5f;
            main.startLifetime = 0.2f;
            slowAmmount += 1f;
            clickCount++;
        }
        else if (clickCount == 1 && GameManager.Instance.coins >= 100)
        {
            GameManager.Instance.DecreaseCoins(100);
            radius += 2.5f;
            main.startLifetime = 0.3f;
            clickCount++;
            slowAmmount += 2f;
        }
        else if (clickCount == 2 && GameManager.Instance.coins >= 150)
        {
            GameManager.Instance.DecreaseCoins(150);
            radius += 2.5f;
            main.startLifetime = 0.4f;
            clickCount++;
            slowAmmount += 2f;
        }
        else
        {
            GameManager.Instance.InsufficientFunds();
        }
    }

 


    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }


}
