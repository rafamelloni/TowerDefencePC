using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Factory<Enemies>
{

    ObjectPool<Enemies> _pool;
    public Enemies prefab;
    public void Awake()
    {
        _pool = new ObjectPool<Enemies>(InstatiatiePrebaf, TurnOn, TurnOff,100);
    }


    void TurnOn(Enemies enemy)
    {

        enemy.TurnOn();
    }
    void TurnOff(Enemies enemy)
    {
        enemy.TurnOff();
    }


    Enemies InstatiatiePrebaf()
    {
        return Instantiate(prefab);
    }
    public override Enemies Create()
    {
        var b = _pool.Get();
        b.Pool = _pool;
        return b;
    }


}
