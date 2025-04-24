using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    // Start is called before the first frame update

    [SerializeField] string androidID = "5730409";
    public static string Interstitial_Android { get { return "Interstitial_Android"; } }
    public static string Rewarded_Android { get { return "Rewarded_Android"; } }
    public static string Banner_Android { get { return "Banner_Android"; } }

    [SerializeField] Ad_Rewarded rewarded;
    [SerializeField] Ad_interstitial interstitial;
    [SerializeField] Ad_Banner banner;

    //private void Start()
    //{
    //    if(!Advertisement.isInitialized && Advertisement.isSupported)
    //    {
    //        Advertisement.Initialize(androidID, true);
    //    }
 
    //}
    void IUnityAdsInitializationListener.OnInitializationComplete()
    {
        // cargar anuncios
        Debug.Log("Unity Ads Initialization Complete.");
        rewarded.Initialize();
        interstitial.Initialize();
        banner.Initialize();

    }

    public void Activebutton()
    {
      rewarded.Activate();
    }

    void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
