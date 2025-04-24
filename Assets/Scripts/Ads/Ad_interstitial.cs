using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ad_interstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public void Initialize()
    {
        Advertisement.Load(AdsManager.Interstitial_Android, this);
    }

    public void Activate()
    {
        Advertisement.Show(AdsManager.Interstitial_Android, this);
    }


    //funciones de carga
    void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
    {
        //print se termino de cargr el anuncio
    }

    void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }
    //

    //funciones que paso con el anuncio



    void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //si fallo
    }

    void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
    {
        //si se esta mostrando
    }

    void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
    {
        //si el usuario hizo click
    }

    void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //si se completo
    }
}
