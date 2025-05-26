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
    private Turret turret;

   



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
                turret = torreQueDisparo;
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
        if (!IsDead) // Evitar registro múltiple
        {
            IsDead = true;
            float vidaAlMorir = Mathf.Max(0, currentLife); // Asegurarnos de que la vida no sea negativa
            Debug.Log($"Enemigo muriendo - Tipo: {type}, Vida actual: {currentLife}, Vida al morir: {vidaAlMorir}");
            StatsManager.Instance.RegisterEnemy(type, vidaAlMorir, transform.position);
            if (turret != null)
            {
                turret.kills++;
            }
            GameManager.Instance.IncreaseCoins(coinsForKill);
            Pool.Return(this);
        }
    }
    public override void TurnOn()
    {
        IsDead = false; // Resetear el estado de muerte cuando el enemigo se activa
        currentLife = RemoteConfigHandler.instance.EnemyHealth; // Resetear la vida
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
