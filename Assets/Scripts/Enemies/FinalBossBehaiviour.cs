using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossBehaviour : MonoBehaviour
{
    public NewEnemySpawner enemySpawner;
    public float spawnDelay = 1f; // Tiempo entre enemigos
    public float spawnRadius = 3f; // Radio de la zona donde se genera el enemigo
    public Transform FinalBoss; // El punto base de donde se spawnean los enemigos


    public float currentBossHealth; // Salud del Final Boss (puedes modificarla o vincularla a su sistema de salud)
    public float BossHealth = 1000f; // Salud del Final Boss (puedes modificarla o vincularla a su sistema de salud)

    private bool bossAlive = true; // Controla si el Final Boss está vivo

    public HealtBarTurret healthBar;

    public Canvas winCanvas;

    void Start()
    {

        currentBossHealth = BossHealth;
        if (enemySpawner == null)
        {
            Debug.LogWarning("EnemySpawner no asignado en FinalBossBehaviour.");
        }
        else
        {
            StartCoroutine(SpawnEnemiesWhileAlive());
        }
    }

    private IEnumerator SpawnEnemiesWhileAlive()
    {
        // Continúa spawneando enemigos mientras el Final Boss esté vivo
        while (bossAlive)
        {
            SpawnEnemies(); // Llama al método que genera enemigos

            // Espera el tiempo de spawn antes de generar otro enemigo
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemies()
    {
        if (enemySpawner != null)
        {
            // Calcula un offset aleatorio dentro de un radio definido
            Vector3 offset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,  // Mantén la altura en Y
                Random.Range(-spawnRadius, spawnRadius)
            );

            // Llamamos al método SpawnFinalWave, pasando la nueva posición con el offset
            enemySpawner.SpawnFinalWave(FinalBoss, offset);
        }
       
    }

    // Método para actualizar la vida del Final Boss y verificar si sigue vivo
    public void TakeDamage(float damage)
    {
        currentBossHealth -= damage;

        
            ApplyDamage(damage);
            bossAlive = true; // El boss ha muerto
            
        
    }

    public void ApplyDamage(float damageAmount)
    {
        currentBossHealth = Mathf.Max(currentBossHealth, 0f);

        healthBar.UpdateHealthBar(BossHealth, currentBossHealth);


        if (currentBossHealth <= 0f)
        {
            bossAlive = false; // El boss ha muert
            winCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            GameManager.Instance.activateloosecanvas();
            gameObject.SetActive(false);
        }
    }
}
