using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicTurret : Turrets
{
    public static event Action<BasicTurret> OnTurretDestroyed;
    public Factory<Bullets> BulletFactory;
    //public PlaceablePlatform platform;
    

    private void Start()
    {
        BulletFactory = FindObjectOfType<BulletFactory>();
        type = TurretType.NormalTurret;

    }
    public override void Shoot() 
    {
        var b= BulletFactory.Create();
        b.transform.position = transform.position;
        b.transform.forward = transform.forward;
        

    }

    public override void Die()
    {
        OnTurretDestroyed?.Invoke(this);
        platform.Vacate(this);
        Destroy(gameObject);
    }

  
}
