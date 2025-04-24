using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyFactory : Factory<BaseNewEnemy>
{
    public BaseNewEnemy prefab;
    ObjectPool<BaseNewEnemy> _pool;

    public void Awake()
    {
        _pool = new ObjectPool<BaseNewEnemy>(InstatiatiePrebaf, TurnOn, TurnOff, 100);
    }

    void TurnOn(BaseNewEnemy enemie)
    {
        enemie.TurnOn();
        
    }
    void TurnOff(BaseNewEnemy enemie)
    {
        enemie.TurnOff();
    }
    BaseNewEnemy InstatiatiePrebaf()
    {
        return Instantiate(prefab);
    }
    public override BaseNewEnemy Create()
    {
        var b = _pool.Get();
        b.Pool = _pool;
        return b;
    }
}
