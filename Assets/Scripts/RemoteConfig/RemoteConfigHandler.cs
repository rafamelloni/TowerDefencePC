using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfigHandler : MonoBehaviour
{
    public struct UserAttributes
    {

    }
    public struct AppAttributes
    {

    }
    public static RemoteConfigHandler instance;

    RemoteConfigService remote;

    public int EnemyHealth = 5;
    int EnemyHealthDefault ;

    public float Speed = 3;
    float SpeedDefault;

    public float EnemySpeed = 5;
    float EnemySpeedDefault = 5;

    public float SpawnRate = 1.3f;
    float SpawnRatedefault= 0;

    public int Weave1 = 1;
    int weave1default;


    private void Awake()
    {
        if (instance== null) instance = this;
        else Destroy(this.gameObject);
        
    }

    public void Start()
    {
        Initialize();
    }
    async Task Initialize()
    {
        Debug.Log("Initialize");
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += FetchCompleted;
        RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
    }

    void FetchCompleted(ConfigResponse response)
    {
        Debug.Log(response.status);
        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
                EnemyHealth = EnemyHealthDefault;
                Speed = SpeedDefault;
                EnemySpeed = EnemySpeedDefault;
                SpawnRate = SpawnRatedefault;
                Weave1 = weave1default;
                Debug.Log("Default");
                break;
            case ConfigOrigin.Cached:
                EnemyHealth = RemoteConfigService.Instance.appConfig.GetInt("EnemyHealth", EnemyHealthDefault);
                Speed = RemoteConfigService.Instance.appConfig.GetFloat("Speed", SpeedDefault);
                EnemySpeed = RemoteConfigService.Instance.appConfig.GetFloat("EnemySpeed", EnemySpeedDefault);
                SpawnRate = RemoteConfigService.Instance.appConfig.GetFloat("SpawnRate", SpawnRatedefault);
                Weave1 = RemoteConfigService.Instance.appConfig.GetInt("Weave1", weave1default);
                Debug.Log("Cached");
                break;
            case ConfigOrigin.Remote:
                EnemyHealth = RemoteConfigService.Instance.appConfig.GetInt("EnemyHealth", EnemyHealthDefault);
                Debug.Log("Remote");
                Speed = RemoteConfigService.Instance.appConfig.GetFloat("Speed", SpeedDefault);
                Debug.Log("RemoteSpeed" + Speed);
                EnemySpeed = RemoteConfigService.Instance.appConfig.GetFloat("EnemySpeed", EnemySpeedDefault);
                Debug.Log("RemoteEnemySpeed" + EnemySpeed);
                SpawnRate = RemoteConfigService.Instance.appConfig.GetFloat("SpawnRate", SpawnRatedefault);
                Debug.Log("the spawnrate is " + SpawnRate);
                Weave1 = RemoteConfigService.Instance.appConfig.GetInt("Weave1", weave1default);
                Debug.Log("the weave is " + Weave1);
                break;
        }
      

    }
}
