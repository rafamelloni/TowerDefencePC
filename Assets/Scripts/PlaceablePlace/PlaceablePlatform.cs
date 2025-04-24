using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceablePlatform : MonoBehaviour
{
    public bool isOccupied = false; // Indica si la plataforma está ocupada
    public bool canBeUpgraded = false;


    public Turrets placedTurret;
    public GameObject circulo;




    public void Occupy(Turrets turret)
    {
        isOccupied = true; // Marca la plataforma como ocupada
        canBeUpgraded = true;
        placedTurret = turret;
        turret.platform = this;

        if (turret is BasicTurret basicTurret)
        {
            basicTurret.platform = this;  // Asocia la plataforma con la torreta
        }
        else if (turret is CannonTurret cannonTurret)
        {
            cannonTurret.platform = this;  // Asocia la plataforma con la torreta
        }
        if(turret is segundoTurret iceTurret)
        {

            iceTurret.platform = this;  // Asocia la plataforma con la torreta
        }
    }

    public void Vacate(Turrets turret)
    {

        if(placedTurret = turret) 
        {
            print("torre deesocupada");
            isOccupied = false;
            canBeUpgraded = false;
            placedTurret = null;
        }
            
     }

    public bool IsTurretOnPlatform(Turrets turret)
    {
        // You may want to check the turret's position or any other condition that confirms the turret is on this platform
        return turret.transform.position == this.transform.position;  // Example check
    }
}
