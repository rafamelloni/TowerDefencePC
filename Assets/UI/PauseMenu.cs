using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas pauseMenuUI;
    bool isPaused = false;

    public static PauseMenu instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


public void OnButtonPress()
    {
        Pause();
    }
    public void OnButtonPress2()
    {
        Resume();
    }

    void Pause()
    {
        pauseMenuUI.gameObject.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0f; // Pausar el juego
        isPaused = true; // Marcar que el juego está pausado
    }

    void Resume()
    {
        pauseMenuUI.gameObject.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = 1f; // Reanudar el juego
        isPaused = false; // Marcar que el juego no está pausado
    }
}
    