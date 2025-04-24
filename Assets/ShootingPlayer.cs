using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayer : MonoBehaviour
{
      public Factory<Bullets> BulletFactory;
    public ParticleSystem particlesSpark;
    public ParticleSystem particlesSmoke;

    public Transform firePos;

    public float fireRate = 0.5f;  // Intervalo entre disparos (en segundos)
    private float lastShotTime = 0f;  // Tiempo del último disparo

    void Start()
    {
        BulletFactory = FindAnyObjectByType<BulletFactory>();
        
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= lastShotTime + fireRate)
        {
            Shoot();
            lastShotTime = Time.time;  // Actualiza el tiempo del último disparo
        }
    }
    public  void Shoot()
    {
        var b = BulletFactory.Create();
        b.transform.position = firePos.transform.position;
        b.transform.forward = firePos.transform.forward;
       
    }
}
