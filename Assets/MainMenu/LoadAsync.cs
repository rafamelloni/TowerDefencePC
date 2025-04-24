using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsync : MonoBehaviour
{
    public static LoadAsync Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressSlider; // Cambiado de Image a Slider

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        // Inicia la carga de la escena
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        // Activa el canvas de carga
        _loaderCanvas.SetActive(true);

        // Actualiza el slider gradualmente
        while (scene.progress < 0.9f)
        {
            _progressSlider.value = Mathf.Clamp01(scene.progress / 0.9f);
            await Task.Yield(); // Espera el siguiente frame
        }

        // Pausa opcional para mostrar la barra completa
        await Task.Delay(500);

        // Permite que la escena se active
        scene.allowSceneActivation = true;

        // Espera hasta que la escena se active completamente
        while (!scene.isDone)
        {
            await Task.Yield();
        }

        // Desactiva el canvas después de que la escena esté completamente activa
        _loaderCanvas.SetActive(false);
    }

}
