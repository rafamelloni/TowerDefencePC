using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public StaminaSystem stamina;
    public void ChangeScene(string scene)
    {
       
            LoadAsync.Instance.LoadScene(scene);
            
        
        
    }
    public void ChangeSceneAJuego(string scene)
    {

        LoadAsync.Instance.LoadScene(scene);
        GameManager.Instance.coins = 0;


    }

    public void CambiarEscena(string scene)
    {
        LoadAsync.Instance.LoadScene(scene);
    }

}
