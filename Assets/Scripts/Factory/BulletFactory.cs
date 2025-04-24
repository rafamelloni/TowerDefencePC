using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : Factory<Bullets>
{
    public Bullets prefab;
    ObjectPool<Bullets> _pool;
    public void Awake()
    {
        _pool = new ObjectPool<Bullets>(InstatiatiePrebaf, TurnOn, TurnOff, 300);
    }

    void TurnOn(Bullets bullet)
    {
        bullet.TurnOn();
    }
    void TurnOff(Bullets bullet)
    {
        bullet.TurnOff();
    }
    Bullets InstatiatiePrebaf() 
    {
        return Instantiate(prefab);
    }
    public override Bullets Create()
    {
        var b = _pool.Get();
        b.Pool = _pool;
        return b;
    }
}
