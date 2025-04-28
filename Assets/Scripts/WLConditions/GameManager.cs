using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int coins= 100;
    public int currency;

    public SaveData data;
    public SaveData dataNew;
    public CloudSave save;
    private List<Enemie> enemies = new List<Enemie>();
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI currencyTxt;

    public TMP_Text upgradeText;

    public Image noFundsImg;

    public TMP_Text lifeTxt;


    public Canvas looseCanvas;


    int deadCounts; // Resetea el contador de muertos en cada llamada



    ////private async void Start()
    ////{
    ////    if (AuthenticationService.Instance.IsSignedIn)
    ////    {
    ////        await LoadCurrency();
    ////        UpdateTextCurrency();
    ////    }

    ////}

    ////private void Update()
    ////{
    ////    CountDeadEnemies();

    ////    if (deadCounts >= 20)
    ////    {
    ////        LoadScene();
    ////    }
    ////}

    ////public async Task LoadCurrency()
    ////{
    ////    try
    ////    {
    ////        dataNew = await save.OnLoadAsync();
    ////        data.currency = dataNew.currency;
    ////    }
    ////    catch (System.Exception e)
    ////    {
    ////        Debug.LogError($"Error loading currency: {e.Message}");
    ////    }
    ////}

    // Tupla para almacenar información sobre las oleadas completadas
    private List<(int waveNumber, int enemiesKilled, int coinsSpent)> completedWaves = new List<(int, int, int)>();

    // Lista para almacenar información sobre las acciones de compra de torretas
    private List<object> turretPurchaseActions = new List<object>();

    private async void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;

            //try
            //{
            //    // Inicializa Unity Services si no están inicializados.
            //    if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
            //    {
            //        await UnityServices.InitializeAsync();
            //    }

            //    // Verifica si el jugador ya está autenticado.
            //    if (!AuthenticationService.Instance.IsSignedIn)
            //    {
            //        await AuthenticationService.Instance.SignInAnonymouslyAsync();
            //        Debug.Log("Usuario autenticado anónimamente.");
            //    }
            //    else
            //    {
            //        Debug.Log("El jugador ya está autenticado.");
            //    }
            //}
            //catch (Exception e)
            //{
            //    Debug.LogError($"Error al inicializar o autenticar: {e.Message}");
            //}
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //funds
    public void InsufficientFunds()
    {
        noFundsImg.gameObject.SetActive(true);
        StartCoroutine(WaitForSeconds(1f));
    }

    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        noFundsImg.gameObject.SetActive(false);
    }
    //

    //life
    public void LoseLife()
    {
        int currentScore = int.Parse(lifeTxt.text);
        currentScore--;
        lifeTxt.text = currentScore.ToString();
        if (currentScore <= 0)
        {
            looseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    public void activateloosecanvas()
    {
        
            looseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        

    }

    public void RestartGame()
    {
        // Desactiva todos los Canvases activos en la escena
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas != looseCanvas)
            {
                canvas.gameObject.SetActive(false);
            }
        }

        // Activa solo el Canvas de "looseCanvas"
        looseCanvas.gameObject.SetActive(true);

        // Restablece el tiempo y recarga la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewSampleScene");
    }
    public void bactToMencu()
    {

        SceneManager.LoadScene("Menu");
    }


    //

    //cuurrency
    public void IncreaseCurrency()
    {
        data.currency += 100;
        UpdateTextCurrency();
        save.Save(data);
    }

    private void UpdateTextCurrency()
    {
        currencyTxt.text = data.currency.ToString();
    }



    //coins
    public void IncreaseCoins(int value)
    {
        coins += value;
        textMeshProUGUI.text = coins.ToString();
    }

    public void DecreaseCoins(int value)
    {
        coins -= value;
        textMeshProUGUI.text = coins.ToString();

        // Registrar el gasto de monedas en CoinSpendingManager
        CoinSpendingManager.Instance.RegisterCoinSpending(value);
    }
    //


    public void RegisterEnemy(Enemie enemy)
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemie enemy)
    {
        enemies.Remove(enemy);
    }
    public void CountDeadEnemies()
    {
        

        // Iterar hacia atrás para evitar problemas al eliminar elementos
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            var enemy = enemies[i];
            if (enemy != null && enemy.IsDead) // Verifica que el enemigo no sea null y si está muerto
            {
                deadCounts++;
                UnregisterEnemy(enemy); // Eliminar el enemigo de la lista
            }
        }
    }

    // Método para registrar una oleada completada
    public void RegisterCompletedWave(int waveNumber, int enemiesKilled, int coinsSpent)
    {
        completedWaves.Add((waveNumber, enemiesKilled, coinsSpent));
    }

    // Método para registrar una acción de compra de torreta
    public void RegisterTurretPurchase(string turretName, int cost)
    {
        var purchaseAction = new
        {
            TurretName = turretName,
            Cost = cost,
            Time = Time.time
        };

        turretPurchaseActions.Add(purchaseAction);
    }

    // Método para obtener todas las acciones de compra de torretas
    public IEnumerable<object> GetTurretPurchaseActions()
    {
        return turretPurchaseActions;
    }
}
