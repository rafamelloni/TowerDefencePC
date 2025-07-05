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
    public List<NewEnemie> enemigosEnEscena;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //Rafael Melloni
        foreach (var enemy in enemyDeaths)
        {
            if(Input.GetKeyDown(KeyCode.Q)) Debug.Log($"Enemy type: {enemy.enemyType}, Health at death: {enemy.healthAtDeath}, Position: {enemy.position}");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TopTorresLetales(3); // o cualquier cantidad
        }

        // Nueva funcionalidad: Mostrar estadísticas de daño por tipo de enemigo
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Tecla R presionada - Calculando estadísticas...");
            var damageStats = GetDamageStatsByEnemyType().Take(10);
            if (!damageStats.Any())
            {
                Debug.Log("No hay datos de daño registrados aún");
            }
            foreach (var stat in damageStats)
            {
                Debug.Log($"Tipo: {stat.enemyType}, Daño promedio: {stat.averageDamage:F2}, Tiempo promedio de vida: {stat.averageLifetime:F2}s");
            }
        }

        //Lautaro Nieto

        // Mostrar el promedio de monedas gastadas por oleada al presionar la tecla T
        if (Input.GetKeyDown(KeyCode.T))
        {
            float averageCoinsSpent = CoinSpendingManager.Instance.GetAverageCoinsSpentPerWave();
            Debug.Log($"Promedio de monedas gastadas por oleada: {averageCoinsSpent:F2}");
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            var waveAnalysis = CoinSpendingManager.Instance.GetWaveSpendingAnalysis();
            Debug.Log("Análisis de gastos por oleada:");
            foreach (var item in waveAnalysis)
            {
                Debug.Log($"Oleada {item.wave}: {item.totalSpent} monedas gastadas");
            }
        }

        // Tecla U: Patrones de gasto
        if (Input.GetKeyDown(KeyCode.U))
        {
            var spendingPatterns = CoinSpendingManager.Instance.GetSpendingPatterns(50);
            Debug.Log("Patrones de gasto (gastos > 50):");
            foreach (var pattern in spendingPatterns)
            {
                Debug.Log($"Grupo {pattern.Key}: {string.Join(", ", pattern.Value)}");
            }
        }

        // Tecla I: Estadísticas complejas
        if (Input.GetKeyDown(KeyCode.I))
        {
            var stats = CoinSpendingManager.Instance.GetComplexSpendingStats();
            Debug.Log($"Estadísticas de gastos: {stats}");
        }

        // Tecla P: Primer gasto registrado
        if (Input.GetKeyDown(KeyCode.P))
        {
            int firstSpending = CoinSpendingManager.Instance.GetFirstCoinSpending();
            Debug.Log($"Primer gasto registrado: {firstSpending}");
        }

        // Mostrar posiciones de muerte de enemigos por tipo al presionar la tecla Y
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("\n=== MOSTRANDO ESTADÍSTICAS DE MUERTES (TECLA Y) ===");
            var deathPositions = GetEnemyDeathPositionsByType();
            if (!deathPositions.Any())
            {
                Debug.Log("No hay datos de posiciones de muerte registrados aún");
            }
            else
            {
                foreach (var kvp in deathPositions)
                {
                    Debug.Log($"\nTipo de enemigo: {kvp.Key}");
                    Debug.Log($"Cantidad de muertes: {kvp.Value.Count}");
                    foreach (var position in kvp.Value)
                    {
                        Debug.Log($"Posición: {position}");
                    }
                }
            }
            Debug.Log("=== FIN DE ESTADÍSTICAS ===\n");

            // Mostrar estadísticas de distancias
            var (distanciaTotal, distanciaPromedio, totalMuertes) = GetEstadisticasDistancias();

            // Mostrar estadísticas detalladas
            var (maxDistancia, minDistancia, promedio) = GetEstadisticasDetalladas();
        }
    }

    public void RegisterEnemy(string type, float healthAtDeath, Vector3 position)
    {
        Debug.Log($"StatsManager - Registrando enemigo muerto - Tipo: {type}, Vida: {healthAtDeath}, Posición: {position}");
        enemyDeaths.Add((type, healthAtDeath, position));
        Debug.Log($"StatsManager - Total de muertes registradas: {enemyDeaths.Count}");
    }

    // Nueva función para registrar daño por enemigo
    public void RegisterEnemyDamage(string type, float damage, float timeOfDeath)
    {
        enemyDamageStats.Add((type, damage, timeOfDeath));
    }


    //Rafael Melloni
    public void TopTorresLetales(int cantidad)
    {
        if (torresEnEscena == null)
        {
            torresEnEscena = new List<Turret>();
            Debug.LogWarning("Lista de torres no inicializada. Se creó una nueva lista vacía.");
            return;
        }

        if (enemigosEnEscena == null)
        {
            enemigosEnEscena = new List<NewEnemie>();
            Debug.LogWarning("Lista de enemigos no inicializada. Se creó una nueva lista vacía.");
            return;
        }

        // Generator
        foreach (var linea in GenerarResumenEstadisticas(cantidad))
        {
            Debug.Log(linea);
        }

        // Aggregate
        int totalDisparos = torresEnEscena
            .Where(t => t != null)
            .Select(t => t.cntador)
            .Aggregate(0, (acum, actual) => acum + actual);

        Debug.Log($"[Aggregate] Total de disparos entre todas las torres: {totalDisparos}");
    }

    // Generator
    public IEnumerable<string> GenerarResumenEstadisticas(int cantidad)
    {
        var torresLetales = torresEnEscena
            .Where(t => t != null && t.gameObject != null && t.gameObject.activeSelf)
            .OrderByDescending(t => t.kills)
            .Take(cantidad);

        foreach (var torre in torresLetales)
        {
            yield return $"[Torre Letal] Nombre: {torre.name}, Kills: {torre.kills}";
        }

        var torresDisparo = torresEnEscena
            .Where(t => t != null && t.gameObject != null && t.gameObject.activeSelf)
            .OrderByDescending(t => t.cntador)
            .Take(cantidad);

        foreach (var torre in torresDisparo)
        {
            yield return $"[Torre Precisa] Nombre: {torre.name}, Disparos: {torre.cntador}";
        }

        var enemigosTop = enemigosEnEscena
            .Where(e => e != null && e.gameObject != null && e.gameObject.activeSelf)
            .OrderByDescending(e => e.TotalDamageDone())
            .Take(cantidad);

        foreach (var enemigo in enemigosTop)
        {
            yield return $"[Enemigo Fuerte] Nombre: {enemigo.name}, Daño: {enemigo.TotalDamageDone()}";
        }
    }



    //

    // Nueva función LINQ con time-slicing y generador Morena Guerra
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

  

    // Guerra Morena
    public Dictionary<string, List<Vector3>> GetEnemyDeathPositionsByType()
    {
        Debug.Log("\n=== INICIO DE ESTADÍSTICAS DE MUERTES ===");
        Debug.Log($"Total de muertes registradas: {enemyDeaths.Count}");
        
        if (enemyDeaths.Count == 0)
        {
            Debug.Log("No hay muertes registradas aún");
            return new Dictionary<string, List<Vector3>>();
        }

        // Mostrar todas las muertes registradas
        Debug.Log("\nTodas las muertes registradas:");
        foreach (var death in enemyDeaths)
        {
            Debug.Log($"Muerte registrada - Tipo: {death.enemyType}, Vida: {death.healthAtDeath}, Pos: {death.position}");
        }

        // Grupo 1: Where (filtrado) - Ahora filtramos por tipo válido
        var muertesFiltradas = enemyDeaths.Where(e => !string.IsNullOrEmpty(e.enemyType));
        Debug.Log($"\nMuertes filtradas (tipo válido): {muertesFiltradas.Count()}");

        // Grupo 2: OrderByDescending (ordenamiento) - Ordenamos por tipo
        var muertesOrdenadas = muertesFiltradas.OrderByDescending(e => e.enemyType);
        
        // Agrupamiento y transformación
        var grupos = muertesOrdenadas.GroupBy(e => e.enemyType);
        Debug.Log($"\nNúmero de grupos por tipo: {grupos.Count()}");

        // Grupo 3: ToDictionary (transformación final)
        var result = grupos.ToDictionary(
            g => g.Key,
            g => g.Select(e => e.position).ToList()
        );

        Debug.Log($"\n=== RESUMEN FINAL ===");
        Debug.Log($"Número de tipos de enemigos encontrados: {result.Count}");
        
        // Verificar cada grupo en el diccionario
        foreach (var kvp in result)
        {
            Debug.Log($"\nTipo: {kvp.Key}");
            Debug.Log($"Cantidad de muertes: {kvp.Value.Count}");
            if (kvp.Value.Count > 0)
            {
                foreach (var pos in kvp.Value)
                {
                    Debug.Log($"  Posición: {pos}");
                }
            }
            else
            {
                Debug.Log("  No hay posiciones registradas para este tipo");
            }
        }
        Debug.Log("=== FIN DE ESTADÍSTICAS DE MUERTES ===\n");

        return result;
    }

    // Nueva función que usa Aggregate para calcular estadísticas
    public (float distanciaTotal, float distanciaPromedio, int totalMuertes) GetEstadisticasDistancias()
    {
        Debug.Log("\n=== ESTADÍSTICAS DE DISTANCIAS ===");
        
        if (enemyDeaths.Count == 0)
        {
            Debug.Log("No hay muertes registradas para calcular estadísticas");
            return (0, 0, 0);
        }

        // Grupo 1: Select para obtener posiciones
        var posiciones = enemyDeaths.Select(e => e.position);
        
        // Grupo 2: OrderByDescending para ordenar por distancia
        var posicionesOrdenadas = posiciones.OrderByDescending(p => p.magnitude);
        
        // Grupo 3: ToList para convertir a lista y poder trabajar con ella
        var listaPosiciones = posicionesOrdenadas.ToList();

        // Calculamos las estadísticas manualmente
        float distanciaTotal = 0f;
        foreach (var pos in listaPosiciones)
        {
            distanciaTotal += pos.magnitude;
        }

        float distanciaPromedio = listaPosiciones.Count > 0 ? distanciaTotal / listaPosiciones.Count : 0;

        Debug.Log($"Distancia total recorrida: {distanciaTotal:F2}");
        Debug.Log($"Distancia promedio: {distanciaPromedio:F2}");
        Debug.Log($"Total de muertes: {listaPosiciones.Count}");
        Debug.Log("=== FIN DE ESTADÍSTICAS DE DISTANCIAS ===\n");

        return (distanciaTotal, distanciaPromedio, listaPosiciones.Count);
    }

    // Nueva función que usa Aggregate y las funciones LINQ permitidas
    public (float maxDistancia, float minDistancia, float promedio) GetEstadisticasDetalladas()
    {
        Debug.Log("\n=== ESTADÍSTICAS DETALLADAS ===");
        
        if (enemyDeaths.Count == 0)
        {
            Debug.Log("No hay muertes registradas para calcular estadísticas");
            return (0, 0, 0);
        }

        // Grupo 1: Select para obtener posiciones
        var posiciones = enemyDeaths.Select(e => e.position);
        
        // Grupo 2: OrderByDescending para ordenar por distancia
        var posicionesOrdenadas = posiciones.OrderByDescending(p => p.magnitude);
        
        // Grupo 3: ToList para convertir a lista
        var listaPosiciones = posicionesOrdenadas.ToList();

        // Usamos Aggregate para calcular estadísticas
        var estadisticas = listaPosiciones.Aggregate(
            new { 
                Max = float.MinValue, 
                Min = float.MaxValue, 
                Sum = 0f, 
                Count = 0 
            },
            (acc, pos) => new {
                Max = Mathf.Max(acc.Max, pos.magnitude),
                Min = Mathf.Min(acc.Min, pos.magnitude),
                Sum = acc.Sum + pos.magnitude,
                Count = acc.Count + 1
            }
        );

        float promedio = estadisticas.Count > 0 ? estadisticas.Sum / estadisticas.Count : 0;

        Debug.Log($"Distancia máxima: {estadisticas.Max:F2}");
        Debug.Log($"Distancia mínima: {estadisticas.Min:F2}");
        Debug.Log($"Distancia promedio: {promedio:F2}");
        Debug.Log($"Total de muertes: {estadisticas.Count}");
        Debug.Log("=== FIN DE ESTADÍSTICAS DETALLADAS ===\n");

        return (estadisticas.Max, estadisticas.Min, promedio);
    }
}
