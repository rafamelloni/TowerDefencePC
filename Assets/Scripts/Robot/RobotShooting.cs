using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public Factory<Bullets> BulletFactory;

    public float fireRate = 0.5f; // Tiempo entre disparos en segundos
    private float nextFireTime = 0f;

    public OrbitRobot speed;
    void Start()
    {
        BulletFactory = FindAnyObjectByType<BulletFactory>();
    }

    // Update is called once per frame
    public void Shoot()
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;

        var b = BulletFactory.Create();
        b.transform.position = transform.position;
        b.transform.forward = transform.forward;
      


    }


}
