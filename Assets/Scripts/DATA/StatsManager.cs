using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    // Lista de tuplas
    private List<(string enemyType, float healthAtDeath, Vector3 position)> enemyDeaths =
        new List<(string, float, Vector3)>();


    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


    }

    public void RegisterEnemy(string type, float healthAtDeath, Vector3 position)
    {
        enemyDeaths.Add((type, healthAtDeath, position));
    }

   
}
