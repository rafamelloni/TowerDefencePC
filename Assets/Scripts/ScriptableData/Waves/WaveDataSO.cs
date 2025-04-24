using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewWave", menuName = "Wave")]
public class WaveDataSO : ScriptableObject
{
    public string waveName; // Nombre de la oleada
    public GameObject enemyPrefab; // Prefab del enemigo
    public int NormalenemyCount; // Cantidad de enemigos
    public int Normal1enemyCount; // Cantidad de enemigos
    public int FastenemyCount; // Cantidad de enemigos
    public float spawnRate; // Tasa de aparición
}
