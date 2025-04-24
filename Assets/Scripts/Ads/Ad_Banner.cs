using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class Ad_Banner : MonoBehaviour
{
    private void Awake()
    {
        Advertisement.Banner.SetPosition(BannerPosition.CENTER);
    }
    public void Initialize()
    {
        BannerLoadOptions load = new BannerLoadOptions();
        load.loadCallback = LoadCallBack;
        load.errorCallback = LoadErrorBack;

        Advertisement.Banner.Load(AdsManager.Banner_Android, load);
    }


    void LoadErrorBack(string msjError)
    {
        print(msjError);
    }


    public void Activate()
    {
        BannerOptions options = new BannerOptions();
        options.showCallback = ShowCallback;
        options.clickCallback = ClickCallback;
        options.hideCallback = HideCallback;

        Advertisement.Banner.Show(AdsManager.Banner_Android, options); 
    }

    public void Hide()
    {
        Advertisement.Banner.Hide();
    }

    //show func
    void ShowCallback()
    {

    }
    void HideCallback()
    {

    }
    void ClickCallback()
    {

    }

    void LoadCallBack()
    {

    }

}
