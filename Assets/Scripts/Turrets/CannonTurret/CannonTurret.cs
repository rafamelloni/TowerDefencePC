using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonTurret : Turrets
{
    public static event Action<CannonTurret> OnTurretDestroyed;
    public Factory<Bullets> BulletFactory;
    //public PlaceablePlatform platform;


    private void Start()
    {
    
        type = TurretType.Cannon;

    }

    public override void Die()
    {
        OnTurretDestroyed?.Invoke(this);
        platform.Vacate(this);
        Destroy(gameObject);
    }

    public override void Shoot()
    {
        throw new NotImplementedException();
    }
}
