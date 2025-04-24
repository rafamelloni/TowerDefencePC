using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenManager : MonoBehaviour, IScreen
{
    public Canvas canvsBuild;
    public Canvas canvsNormalTurret;
    public Canvas canvsMachinegunTurret;
    public Canvas canvsIcelTurret;
    public void Start()
    {
        if (canvsBuild == null || canvsNormalTurret == null || canvsMachinegunTurret == null || canvsIcelTurret == null)
        {
            
            return;
        }
       

        canvsBuild.gameObject.SetActive(false);
        canvsNormalTurret.gameObject.SetActive(false);
        canvsMachinegunTurret.gameObject.SetActive(false);
        canvsIcelTurret.gameObject.SetActive(false);
    }

    public void Show(Canvas canvs)
    {
        if (canvs != null)
        {
            canvs.gameObject.SetActive(true);
        }

    }

    public void Hide(Canvas canvs)
    {
        if (canvs != null)
        {
            canvs.gameObject.SetActive(false);
        }

    }
}


