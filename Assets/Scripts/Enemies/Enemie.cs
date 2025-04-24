using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;



public class Enemie : Enemies
{


    //private float _maxLife = RemoteConfigHandler.instance.EnemyHealth;
    public float currentLife;
    // Si el shader está en r;
    private int coinsForKill = 10;
    public bool IsDead { get; private set; }

   



    void Start()
    {
        currentLife = RemoteConfigHandler.instance.EnemyHealth;
        
     

    }

    public void TakeDmg(float dmg)
    {
        currentLife-= dmg;
        
        

        if (currentLife <= 0)
        {
            //Death();
            StartCoroutine(DeathCoroutine());
        }
    }
    

    private IEnumerator DeathCoroutine()
    {

        

        // Espera 2 segundos
        yield return new WaitForSeconds(0.5f);

        // Llama a la función de muerte

        Death();
        
    }

    void Death()
    {
        IsDead = true;
        Pool.Return(this);
        GameManager.Instance.IncreaseCoins(coinsForKill);
        StatsManager.Instance.RegisterEnemy("Normal", currentLife, transform.position);
    }
    public override void TurnOn()
    {
        GameManager.Instance.RegisterEnemy(this);
        base.TurnOn();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            GameManager.Instance.LoseLife();
            Pool.Return(this);
        }
    }



}
