using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    // Lista de tuplas
    private List<(string enemyType, float healthAtDeath, Vector3 position)> enemyDeaths =
        new List<(string, float, Vector3)>();

    //Lista de torretas
    public List<Turret> torresEnEscena;


    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


    }
    private void Update()
    {
        foreach (var enemy in enemyDeaths)
        {
            if(Input.GetKeyDown(KeyCode.Q)) Debug.Log($"Enemy type: {enemy.enemyType}, Health at death: {enemy.healthAtDeath}, Position: {enemy.position}");

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            var top = TopTorresLetales(3);
            foreach (var t in top)
            {
                Debug.Log($" {t.nombre} — {t.kills} kills");
            }
        }
    }

    public void RegisterEnemy(string type, float healthAtDeath, Vector3 position)
    {
        enemyDeaths.Add((type, healthAtDeath, position));
    }

    public List<(string nombre, int kills)> TopTorresLetales(int cantidad)
    {
        return torresEnEscena
            .Where(t => t.gameObject.activeSelf)
            .OrderByDescending(t => t.kills)
            .Take(cantidad)
            .Select(t => (t.name, t.kills))
            .ToList();
    }


}
