using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class CoinSpendingManager : MonoBehaviour
{
    public static CoinSpendingManager Instance { get; private set; }

    // Lista para registrar el gasto de monedas por oleada
    private List<int> coinsSpentPerWave = new List<int>();
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
        coinsSpentPerWave.Add(coinsSpent);
    }

    // Método para obtener el promedio de monedas gastadas por oleada
    public float GetAverageCoinsSpentPerWave()
    {
        // Grupo 1: Where
        var filteredWaves = coinsSpentPerWave
            .Where(coinsSpent => coinsSpent > 0); // Filtrar gastos válidos

        // Grupo 2: OrderBy
        var orderedWaves = filteredWaves
            .OrderBy(coinsSpent => coinsSpent); // Ordenar por gasto

        // Grupo 3: ToList
        var waveList = orderedWaves.ToList(); // Convertir a lista

        if (waveList.Count == 0) return 0;

        return waveList.Sum() / waveList.Count;
    }

    public List<(int wave, int totalSpent)> GetWaveSpendingAnalysis()
    {
        var validSpendings = coinsSpentPerWave
            .Where(spent => spent > 0); // Grupo 1: Where

        var orderedSpendings = validSpendings
            .OrderByDescending(spent => spent); // Grupo 2: OrderByDescending

        return orderedSpendings
            .Select((spent, index) => (wave: index + 1, totalSpent: spent))
            .ToList(); // Grupo 3: ToList
    }

    public Dictionary<int, List<int>> GetSpendingPatterns(int threshold)
    {
        return coinsSpentPerWave
         .TakeWhile(spent => spent > threshold)
         .Select((spent, index) => new { Value = spent, Group = (index / 3) + 1 })
         .GroupBy(item => item.Group, item => item.Value)
         .ToDictionary(g => g.Key, g => g.ToList());
    }



    public int GetFirstCoinSpending()
    {
        return coinsSpentPerWave.FirstOrDefault();
    }


    public IEnumerable<int> GetOrderedCoinSpendings()
    {
        return coinsSpentPerWave
            .OrderBy(coinsSpent => coinsSpent)
            .ThenBy(coinsSpent => coinsSpent);
    }


    public object GetComplexSpendingStats()
    {
        var stats = coinsSpentPerWave.Aggregate(
            new
            {
                Total = 0,
                Max = int.MinValue,
                Min = int.MaxValue,
                Count = 0,
                SumSquares = 0
            },
            (acc, coinsSpent) => new {
                Total = acc.Total + coinsSpent,
                Max = coinsSpent > acc.Max ? coinsSpent : acc.Max,
                Min = coinsSpent < acc.Min ? coinsSpent : acc.Min,
                Count = acc.Count + 1,
                SumSquares = acc.SumSquares + (coinsSpent * coinsSpent)
            });

        float average = stats.Count > 0 ? (float)stats.Total / stats.Count : 0;
        float stdDev = stats.Count > 1 ?
            Mathf.Sqrt((stats.SumSquares / stats.Count) - (average * average)) : 0;

        return new
        {
            Total = stats.Total,
            Average = average,
            Maximum = stats.Max,
            Minimum = stats.Min,
            StandardDeviation = stdDev,
            Count = stats.Count
        };
    }


    // Generador para obtener gastos de monedas
    public IEnumerable<int> GetCoinSpendingsGenerator()
    {
        foreach (var coinsSpent in coinsSpentPerWave)
        {
            yield return coinsSpent;
        }
    }

    // Tipo anónimo para almacenar información sobre gastos de monedas
    public object GetCoinSpendingInfo()
    {
        var coinSpendingInfo = new
        {
            TotalCoinsSpent = coinsSpentPerWave.Sum(),
            AverageCoinsSpent = coinsSpentPerWave.Average(),
            MaxCoinsSpent = coinsSpentPerWave.Max(),
            MinCoinsSpent = coinsSpentPerWave.Min()
        };

        return coinSpendingInfo;
    }
}



