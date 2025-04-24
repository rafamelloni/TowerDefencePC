using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ad_Rewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public StaminaSystem stamina;
    public void Initialize()
    {
        Advertisement.Load(AdsManager.Rewarded_Android, this);
    }

  public void Activate()
    {
        Advertisement.Show(AdsManager.Rewarded_Android, this);
    }

    //las de carga
    void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
    {
    }

    void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    //las de mostrar
    void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //si fallo
    }

    void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
    {
        //si comenzo
    }

    void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
    {
        //si clickeo
    }

    void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //si se completio
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                stamina.AddStamina(1);
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
        }
    }
    //
}
