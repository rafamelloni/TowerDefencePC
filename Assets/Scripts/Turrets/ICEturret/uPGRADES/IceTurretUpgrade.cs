using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IceTurretUpgrade : MonoBehaviour
{
    public segundoTurret currentTurret;
    public Transform Player;

    public Image[] upgradeImages;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.gray;

    private segundoTurret lastSelectedTurret;
    private List<segundoTurret> iceTurrets = new List<segundoTurret>();
    private int lastClickCount = -1;

    public TMP_Text upgradeText;
    public TMP_Text upgradeTextInfo;

    private void Awake()
    {
        
        QualitySettings.vSyncCount = 0;
    }

    public void AddTurretToList(segundoTurret turret)
    {
        if (turret != null && !iceTurrets.Contains(turret))
        {
            iceTurrets.Add(turret); // Agregar la torreta a la lista si no está ya en ella
        }
    }

    private void Update()
    {
        
        segundoTurret nearestTurret = null;
        float minDistSqr = 25f; // 5f * 5f (usamos `sqrMagnitude`)

        foreach (var turret in iceTurrets)
        {
            if (turret == null) continue;
            float distSqr = (turret.transform.position - Player.position).sqrMagnitude;
            if (distSqr < minDistSqr)
            {
                nearestTurret = turret;
                break;
            }
        }

        if (nearestTurret != null && nearestTurret != lastSelectedTurret)
        {
            currentTurret = nearestTurret;
            lastSelectedTurret = nearestTurret;
            lastClickCount = -1;
            UpdateUpgradeImages();
        }
    }

    private void UpdateUpgradeImages()
    {
        if (currentTurret == null || currentTurret.clickCount == lastClickCount)
            return;

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
                upgradeText.text = "100"; // Precio de la mejora nivel 1
                upgradeTextInfo.text = "Increase Radius and SlowAmmount"; // Precio de la mejora nivel 1
                break;
            case 2:
                upgradeText.text = "150"; // Precio de la mejora nivel 2
                upgradeTextInfo.text = "Increase Radius and SlowAmmount"; // Precio de la mejora nivel 1
                break;
            case 3:
                upgradeText.text = "MaxLevel"; // Precio de la mejora nivel 3
                upgradeTextInfo.text = "Max Level"; // Precio de la mejora nivel 1
                break;
            default:
                upgradeText.text = "50"; // Precio por defecto (nivel 0)
                upgradeTextInfo.text = "Increase Radius and SlowAmmount"; // Precio de la mejora nivel 1
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
