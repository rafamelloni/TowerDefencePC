using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;



public class Enemie : Enemies
{


    //private float _maxLife = RemoteConfigHandler.instance.EnemyHealth;
    public float currentLife;
    // Si el shader est� en r;
    private int coinsForKill = 10;
    public bool IsDead { get; private set; }
    public string type = "Normal";

   



    void Start()
    {
        currentLife = RemoteConfigHandler.instance.EnemyHealth;
        
     

    }

    public void TakeDmg(float dmg, Turret torreQueDisparo = null)
    {
        currentLife -= dmg;

        // Registrar el daño en el StatsManager
        StatsManager.Instance.RegisterEnemyDamage(type, dmg, Time.time);

        if (currentLife <= 0)
        {
            if (torreQueDisparo != null)
            {
                torreQueDisparo.kills++;
            }
            StartCoroutine(DeathCoroutine());
        }
    }
    

    private IEnumerator DeathCoroutine()
    {

        

        // Espera 2 segundos
        yield return new WaitForSeconds(0.5f);

        // Llama a la funci�n de muerte

        Death();
        
    }

    void Death()
    {
        IsDead = true;
        StatsManager.Instance.RegisterEnemy(type, currentLife, transform.position);
        Pool.Return(this);
        GameManager.Instance.IncreaseCoins(coinsForKill);
       
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
