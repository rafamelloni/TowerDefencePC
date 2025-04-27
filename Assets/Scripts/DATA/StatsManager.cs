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

    // Nueva tupla para almacenar información de daño por tipo de enemigo
    private List<(string enemyType, float damageTaken, float timeOfDeath)> enemyDamageStats =
        new List<(string, float, float)>();

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
                Debug.Log($" {t.nombre} {t.kills} kills");
            }
        }

        // Nueva funcionalidad: Mostrar estadísticas de daño por tipo de enemigo
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Tecla R presionada - Calculando estadísticas...");
            var damageStats = GetDamageStatsByEnemyType();
            if (!damageStats.Any())
            {
                Debug.Log("No hay datos de daño registrados aún");
            }
            foreach (var stat in damageStats)
            {
                Debug.Log($"Tipo: {stat.enemyType}, Daño promedio: {stat.averageDamage:F2}, Tiempo promedio de vida: {stat.averageLifetime:F2}s");
            }
        }
    }

    public void RegisterEnemy(string type, float healthAtDeath, Vector3 position)
    {
        enemyDeaths.Add((type, healthAtDeath, position));
    }

    // Nueva función para registrar daño por enemigo
    public void RegisterEnemyDamage(string type, float damage, float timeOfDeath)
    {
        enemyDamageStats.Add((type, damage, timeOfDeath));
    }

    public List<(string nombre, int kills)> TopTorresLetales(int cantidad)
    {
        // Asegurarse de que la lista no sea null
        if (torresEnEscena == null)
        {
            torresEnEscena = new List<Turret>();
            Debug.LogWarning("Lista de torres no inicializada. Se creó una nueva lista vacía.");
            return new List<(string, int)>();
        }

        // Filtrar torretas null y activas
        return torresEnEscena
            .Where(t => t != null && t.gameObject != null && t.gameObject.activeSelf)
            .OrderByDescending(t => t.kills)
            .Take(cantidad)
            .Select(t => (t.name, t.kills))
            .ToList();
    }

    // Nueva función LINQ con time-slicing y generador
    public IEnumerable<(string enemyType, float averageDamage, float averageLifetime)> GetDamageStatsByEnemyType()
    {
        // Time-slicing: Agrupar por intervalos de tiempo
        var timeIntervals = enemyDamageStats
            .GroupBy(e => Mathf.FloorToInt(e.timeOfDeath / 10f)) // Agrupar en intervalos de 10 segundos
            .Select(g => new
            {
                TimeInterval = g.Key,
                Stats = g.GroupBy(e => e.enemyType)
                    .Select(eg => new
                    {
                        EnemyType = eg.Key,
                        AverageDamage = eg.Average(e => e.damageTaken),
                        AverageLifetime = eg.Average(e => e.timeOfDeath)
                    })
            });

        // Generador: Crear secuencia de estadísticas
        foreach (var interval in timeIntervals)
        {
            foreach (var stat in interval.Stats)
            {
                yield return (stat.EnemyType, stat.AverageDamage, stat.AverageLifetime);
            }
        }
    }

    // Nueva función LINQ que combina Where, OrderBy y ToDictionary
    public Dictionary<string, List<Vector3>> GetEnemyDeathPositionsByType()
    {
        return enemyDeaths
            .Where(e => e.healthAtDeath > 0) // Solo enemigos que murieron con vida
            .OrderByDescending(e => e.healthAtDeath) // Ordenar por vida al morir
            .GroupBy(e => e.enemyType)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.position).ToList()
            );
    }
}
