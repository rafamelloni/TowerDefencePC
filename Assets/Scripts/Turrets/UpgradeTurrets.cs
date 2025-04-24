using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTurrets : MonoBehaviour
{
    public Canvas canvsNormalTurret;
    public Canvas canvsCannonTurret;
    public Canvas canvsIcelTurret;

    public ScreenManager show;
    //ON CANVAS
    public void CanvasNormalTurret()
    {
        show.Show(canvsNormalTurret);
        
    }
    public void CanvasIcelTurret()
    {
        show.Show(canvsIcelTurret);
    }

    public void CanvasMachinegunlTurret()
    {
        
        show.Show(canvsCannonTurret);
    }

    public void CanvasOFFNormalTurret()
    {
        show.Hide(canvsNormalTurret);
    }
    public void CanvasOFFIcelTurret()
    {

        show.Hide(canvsIcelTurret);
    }

    public void CanvasOFFMachinegunlTurret()
    {
        show.Hide(canvsCannonTurret);
    }

    public void DesactivateCanvases()
    {
        CanvasOFFNormalTurret();
        CanvasOFFIcelTurret();
        CanvasOFFMachinegunlTurret();


    }


    //OFF CANVAS

    
}