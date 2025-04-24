using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinTurret : Turrets
{

    public Factory<Bullets> BulletFactory;

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        var b = BulletFactory.Create();
        b.transform.position = transform.position;
        b.transform.forward = transform.forward;
    }



    // Start is called before the first frame update
    void Start()
    {
        type = TurretType.Machinegun;
        BulletFactory = FindObjectOfType<BulletFactory>();
       
    }

}
