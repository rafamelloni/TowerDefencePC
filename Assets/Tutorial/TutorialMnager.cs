using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;

public class TutorialMnager : MonoBehaviour
{
    public GameObject[] popUps; // Array de pop-ups de UI
    private int popUpIndex = 0; // Índice del paso actual del tutorial

    private bool pressedW, pressedA, pressedS, pressedD;

    public PlaceablePlatform platform;

    //paso9: Next wave
    public Button nextWaveButton;
    public Image nextWaveButtonImg;

    //paso 8: robot

    public GameObject robotCircle;

    // shop
    public GameObject shopCircle;
    public EnterShop shop;
    


    // Cámaras PARA EL PASO 6
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera camSpawner;
    public CinemachineVirtualCamera camBase;

    public GameObject spawnerTXT;
    public GameObject spawnerTXT1;
    public GameObject baseTXT;


    //paso 5
    public ActivateTrap trap;
    public GameObject trapCircle;


    private bool isShowingSpawners = false; // Para evitar múltiples corutinas

    // Lista de acciones para los pasos del tutorial
    private List<Action> tutorialSteps = new List<Action>();

    void Start()
    {
        // Desactivar cámaras adicionales al inicio
        camBase.gameObject.SetActive(false);
        camSpawner.gameObject.SetActive(false);

        nextWaveButton.interactable = false;

        // Definir los pasos del tutorial en orden
        tutorialSteps.Add(TextIntro);
        tutorialSteps.Add(StepMovement);
        tutorialSteps.Add(StepPlaceTurret);
        tutorialSteps.Add(TurretUpgradesTUT);
        tutorialSteps.Add(MostrarTrampas);
        tutorialSteps.Add(TiempoDeEsperaParaQueQuedeLindo);
        tutorialSteps.Add(StepShowSpawners);
        tutorialSteps.Add(StepShowRobot);
        tutorialSteps.Add(StepShop);
        tutorialSteps.Add(StepPressNextave);
        tutorialSteps.Add(StepShowNewE);
        tutorialSteps.Add(StepStartGame);

        ShowPopUp(); // Activar el primer pop-up
    }

    void Update()
    {
        if (popUpIndex >= popUps.Length) return; // Evitar errores

        // Activar solo el pop-up actual
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }

        // Ejecutar la lógica del paso actual
        if (popUpIndex < tutorialSteps.Count)
        {
            tutorialSteps[popUpIndex]?.Invoke();
        }
    }

    // Paso 1: Introducción con texto
    void TextIntro()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            NextStep();
        }
    }

    // Paso 2: Movimiento con WASD
    void StepMovement()
    {
        if (Input.GetKeyDown(KeyCode.W)) pressedW = true;
        if (Input.GetKeyDown(KeyCode.A)) pressedA = true;
        if (Input.GetKeyDown(KeyCode.S)) pressedS = true;
        if (Input.GetKeyDown(KeyCode.D)) pressedD = true;

        if (pressedW && pressedA && pressedS && pressedD)
        {
            NextStep();
        }
    }

    // Paso 3: Colocar una torreta
    void StepPlaceTurret()
    {
        if (platform.isOccupied)
        {
            NextStep();
        }
    }

    // Paso 4: Mejoras de la torreta
    void TurretUpgradesTUT()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            NextStep();
        }
    }


    // Paso 5: Mejoras de la torreta
    void MostrarTrampas()
    {
        trapCircle.gameObject.SetActive(true);

        if (trap.trapActivated) // Click izquierdo
        {
            trapCircle.gameObject.SetActive(false);
            NextStep();
        }
    }
    // Paso 6: Tiempo de espera
    void TiempoDeEsperaParaQueQuedeLindo()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            NextStep();
        }
    }

    // Paso 7: Mostrar los spawners con transición de cámaras
    void StepShowSpawners()
    {
        if (!isShowingSpawners)
        {
            StartCoroutine(ShowSpawners());
        }
    }


    // Paso 8: Mostrar el robot
    void StepShowRobot()
    {
        robotCircle.gameObject.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {

            NextStep();
            robotCircle.gameObject.SetActive(false);
        }
    }


    //shop
    void StepShop()
    {
        shopCircle.gameObject.SetActive(true);
        if (shop.enteredShop)
        {
            NextStep();
            shopCircle.gameObject.SetActive(false);
        }
    }

    // Paso 9: LLmar Primera Oleada
    void StepPressNextave()
    {
        nextWaveButton.onClick.RemoveListener(NextStep);

        nextWaveButton.interactable= true;
       

        nextWaveButton.onClick.AddListener(NextStep);
        
    }

    //paso 10;
    void StepShowNewE()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
    }

    //paso 10: Mostrar el nuevoo enemigo

    void StepStartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
    }

    IEnumerator ShowSpawners()
    {
        isShowingSpawners = true;



        //activar los textos del spawner
        spawnerTXT.gameObject.SetActive(true);
        spawnerTXT1.gameObject.SetActive(true);
        // 1. Desactivar mainCam y activar camSpawner
        mainCam.gameObject.SetActive(false);
        camSpawner.gameObject.SetActive(true);

        

        Debug.Log("Activando camSpawner");

        yield return new WaitForSeconds(4); // Esperar 2 segundos

        //desactivar los textos del spawner
        spawnerTXT.gameObject.SetActive(false);
        spawnerTXT1.gameObject.SetActive(false);

        //activar texto de la base
        baseTXT.gameObject.SetActive(true);
        // 2. Desactivar camSpawner y activar camBase
        camSpawner.gameObject.SetActive(false);
        camBase.gameObject.SetActive(true);
        Debug.Log("Activando camBase");

        yield return new WaitForSeconds(4); // Esperar 2 segundos
        //desactivar texto de la base
        baseTXT.gameObject.SetActive(false);
        // 3. Desactivar camBase y reactivar mainCam
        camBase.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        Debug.Log("Reactivating mainCam");

        //isShowingSpawners = false;
        NextStep(); // Pasar al siguiente paso
    }

    // Avanzar al siguiente paso
    void NextStep()
    {
        if (popUpIndex < popUps.Length - 1)
        {
            popUpIndex++;
            ShowPopUp();
        }
    }

    // Mostrar el pop-up correspondiente al paso actual
    void ShowPopUp()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }
    }
}