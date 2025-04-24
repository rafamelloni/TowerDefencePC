using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class TurretUpgradeManager : MonoBehaviour
{
    public Turret currentTurret;
    public Transform Player;

    public Image[] upgradeImages;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.gray;

    private Turret lastSelectedTurret;
    private List<Turret> turrets = new List<Turret>();  // Guardar referencia a las torretas
    private int lastClickCount = -1;  // Guardar último `clickCount` para evitar updates innecesarios

    public TMP_Text upgradeText;

    public TMP_Text upgradeTextInfo;



    public void AddTurretToList(Turret turret)
    {
        if (turret != null && !turrets.Contains(turret))
        {
            turrets.Add(turret); // Agregar la torreta a la lista si no está ya en ella
        }
    }


    private void Update()
    {
        
        Turret nearestTurret = null;
        float minDistSqr = 25f; // 5f * 5f (usamos `sqrMagnitude` en vez de `Distance()`)
       
        
            foreach (var turret in turrets)
            {
            if (turret == null) continue;
                float distSqr = (turret.transform.position - Player.position).sqrMagnitude;
                if (distSqr < minDistSqr)
                {
                    nearestTurret = turret;
                    break;  // Salimos al encontrar la primera torreta cercana
                }
            }

        

        if (nearestTurret != null && nearestTurret != lastSelectedTurret)
        {
            currentTurret = nearestTurret;
            lastSelectedTurret = nearestTurret;
            lastClickCount = -1;  // Forzar actualización de imágenes
            UpdateUpgradeImages();
        }
    }

    private void UpdateUpgradeImages()
    {
        if (currentTurret == null || currentTurret.clickCount == lastClickCount)
            return; // Evita actualizar si el `clickCount` no ha cambiado

        lastClickCount = currentTurret.clickCount;

        UpdateUpgradeText();

        for (int i = 0; i < upgradeImages.Length; i++)

        {
            upgradeImages[i].color = (i < lastClickCount) ? activeColor : inactiveColor;
           
        }
    }


    private void UpdateUpgradeText()
    {
        // Actualiza el texto dependiendo del nivel de mejora
        switch (lastClickCount)
        {
            case 1:
                upgradeText.text = "70"; // Precio de la mejora nivel 1
                upgradeTextInfo.text = "Increase RotationSpeed"; // Precio de la mejora nivel 1

                break;
            case 2:
                upgradeText.text = "90"; // Precio de la mejora nivel 2
                upgradeTextInfo.text = "Increase Detection Area"; // Precio de la mejora nivel 1
                
                break;
            case 3:
                upgradeText.text = "MaxLevel"; // Precio de la mejora nivel 3
                upgradeTextInfo.text = "Max Level"; // Precio de la mejora nivel 1
                break;
            default:
                upgradeText.text = "50"; // Precio por defecto (nivel 0)
                upgradeTextInfo.text = "Increase Fire Rate"; // Precio de la mejora nivel 1
                break;
        }
    }

    public void OnUpgradeButtonPressed()
    {
        
        if (currentTurret != null)
        {
            currentTurret.Upgrade();
            UpdateUpgradeImages();
        }
    }


}
