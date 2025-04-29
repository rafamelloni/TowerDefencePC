using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinSpendingManager : MonoBehaviour
{
    public static CoinSpendingManager Instance { get; private set; }

    // Lautaro Nieto
    private List<(int wave, int coinsSpent)> coinsSpentPerWave = new List<(int, int)>();
    private int currentWave = 1; // Suponiendo que comienzas en la oleada 1

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para avanzar a la siguiente oleada
    public void NextWave()
    {
        currentWave++;
    }

    // Método para registrar el gasto de monedas
    public void RegisterCoinSpending(int coinsSpent)
    {
        coinsSpentPerWave.Add((currentWave, coinsSpent));
    }

    // Método para obtener el promedio de monedas gastadas por oleada
    public float GetAverageCoinsSpentPerWave()
    {
        // Grupo 1: Where
        var filteredWaves = coinsSpentPerWave
            .Where(entry => entry.wave > 0); // Filtrar oleadas válidas

        // Grupo 2: OrderBy
        var orderedWaves = filteredWaves
            .OrderBy(entry => entry.wave); // Ordenar por oleada

        // Grupo 3: ToList
        var waveList = orderedWaves.ToList(); // Convertir a lista

        if (waveList.Count == 0) return 0;

        float totalCoinsSpent = 0;
        foreach (var entry in waveList)
        {
            totalCoinsSpent += entry.coinsSpent;
        }

        int totalWaves = waveList.Select(entry => entry.wave).Distinct().Count();

        return totalWaves > 0 ? totalCoinsSpent / totalWaves : 0;
    }
}
