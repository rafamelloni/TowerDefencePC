using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawner : Spawner
{
    public Factory<BaseNewEnemy> enemyFactory;

    public Transform[] turrets;


    public Transform[] spawnPoints;

    public Transform player;
    public Transform FinalBoss;



    

    public override void Spawn()
    {
        var enemy = enemyFactory.Create();
        enemy.transform.position = transform.position; // Coloca el enemigo en la posici�n del spawner

        // Aseg�rate de que el enemigo est� activo
        enemy.gameObject.SetActive(true);
        enemy.SetTargets( player);
    }
    public  void SpawnFinalWave(Transform FinalBoss, Vector3 offset)
    {
        var enemy = enemyFactory.Create();
        enemy.transform.position = FinalBoss.position + offset; // Coloca el enemigo en la posici�n del spawner
        enemy.transform.position = new Vector3(enemy.transform.position.x, 0.7f, enemy.transform.position.z); // Cambiar la posici�n Y del enemigo


        // Aseg�rate de que el enemigo est� activo
        enemy.gameObject.SetActive(true);
        enemy.SetTargets(player);
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyFactory = FindAnyObjectByType<NewEnemyFactory>();
    }
}
