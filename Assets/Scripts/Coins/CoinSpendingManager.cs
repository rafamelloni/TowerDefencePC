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

        float totalCoinsSpent = 0;
        foreach (var coinsSpent in waveList)
        {
            totalCoinsSpent += coinsSpent;
        }

        int totalWaves = waveList.Count;

        return totalWaves > 0 ? totalCoinsSpent / totalWaves : 0;
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


    public int GetTotalCoinSpending()
    {
        return coinsSpentPerWave.Aggregate(0, (total, next) => total + next);
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


