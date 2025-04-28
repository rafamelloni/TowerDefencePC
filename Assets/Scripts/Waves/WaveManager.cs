using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public WaveDataSO[] waves;
    public EnemySpawner enemySpawner;
    public EnemySpawner enemySpawner1;
    public NewEnemySpawner FastE;
    public Button nextWaveButton;
    [HideInInspector] public int currentWaveIndex = 0;

    public Image img;
    
    public TMP_Text wavesInfo;

    public FollowWaypoints enemi;

    //boss
    public Image imgBoss;
    public GameObject boss;
    private int boolBossIndex;


    // Añadir variables para controlar los tiempos de cada spawner
    public float enemySpawnerTimeFactor = 1f; // Controla el tiempo de spawn del primer spawner
    public float enemySpawner1TimeFactor = 1f; // Controla el tiempo de spawn del segundo spawner
    public float fastEnemySpawnerTimeFactor = 1f; // Controla el tiempo de spawn del spawner rápido

    void Start()
    {
        nextWaveButton.gameObject.SetActive(true);
        img.gameObject.SetActive(true);
        nextWaveButton.onClick.AddListener(OnNextWaveButtonClicked);

        StartCoroutine(ManageWaves());
    }

    private IEnumerator ManageWaves()
    {
        yield return new WaitUntil(() => !nextWaveButton.gameObject.activeSelf);


        while (currentWaveIndex <= waves.Length)
        {
            wavesInfo.text = (currentWaveIndex + 1).ToString();
            if (currentWaveIndex >= waves.Length)
            {
                StartCoroutine(SpawnBoss());


                Debug.Log("aca spawnee boss");
                break;
            }

            // Esperamos a que todos los enemigos de la oleada sean generados
            float totalWaveTime = GetTotalWaveTime(waves[currentWaveIndex]);

            // Usamos una corrutina para spawn de enemigos con el factor de tiempo
            StartCoroutine(SpawnEnemies(enemySpawner, waves[currentWaveIndex].NormalenemyCount, waves[currentWaveIndex].spawnRate, enemySpawnerTimeFactor));
            StartCoroutine(SpawnEnemies(enemySpawner1, waves[currentWaveIndex].Normal1enemyCount, waves[currentWaveIndex].spawnRate, enemySpawner1TimeFactor));
            StartCoroutine(SpawnEnemies(FastE, waves[currentWaveIndex].FastenemyCount, waves[currentWaveIndex].spawnRate, fastEnemySpawnerTimeFactor));

            // Esperamos hasta que haya pasado el tiempo total de la oleada antes de activar el botón
            yield return new WaitForSeconds(totalWaveTime);

            // Activamos el botón para la siguiente oleada
            nextWaveButton.gameObject.SetActive(true);
            img.gameObject.SetActive(true);

            // Esperamos hasta que el botón sea presionado
            yield return new WaitUntil(() => !nextWaveButton.gameObject.activeSelf);
            currentWaveIndex++;
            boolBossIndex++;
            print(currentWaveIndex);

            // Avanzar a la siguiente oleada en CoinSpendingManager
            CoinSpendingManager.Instance.NextWave();

        }
        
       


    }

    private float GetTotalWaveTime(WaveDataSO wave)
    {
        // Calcula el tiempo total que tomará spawnear todos los enemigos
        float normalEnemiesTime = wave.NormalenemyCount * wave.spawnRate * enemySpawnerTimeFactor;
        float normal1EnemiesTime = wave.Normal1enemyCount * wave.spawnRate * enemySpawner1TimeFactor;
        float fastEnemiesTime = wave.FastenemyCount * wave.spawnRate * fastEnemySpawnerTimeFactor;

        // Devuelve el tiempo máximo que tomará la oleada en completarse
        return Mathf.Max(normalEnemiesTime, normal1EnemiesTime, fastEnemiesTime);
    }

    private IEnumerator SpawnEnemies(EnemySpawner spawner, int count, float spawnRate, float timeFactor)
    {
        for (int i = 0; i < count; i++)
        {
            spawner.Spawn();
            yield return new WaitForSeconds(spawnRate * timeFactor); // Aplica el factor de tiempo
        }
    }

    private IEnumerator SpawnEnemies(NewEnemySpawner spawner, int count, float spawnRate, float timeFactor)
    {
        for (int i = 0; i < count; i++)
        {
            spawner.Spawn();
            yield return new WaitForSeconds(spawnRate * timeFactor); // Aplica el factor de tiempo
        }
    }

    private void OnNextWaveButtonClicked()
    {
        
       
            // Si aún hay oleadas, ocultamos el botón de la siguiente oleada
            nextWaveButton.gameObject.SetActive(false);
            img.gameObject.SetActive(false);
            Debug.Log("aca desactive  todo");
            print("wave index de la funcion " + currentWaveIndex);
            print("wave.length print " + waves.Length);
        
    }

    private IEnumerator SpawnBoss()
    {
        print("boss spawning");
        imgBoss.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        imgBoss.gameObject.SetActive(false);
        boss.gameObject.SetActive(true);
        print("boss spawned");
    }
}
