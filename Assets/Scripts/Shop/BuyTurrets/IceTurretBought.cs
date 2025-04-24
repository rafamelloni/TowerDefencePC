using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class IceTurretBought : MonoBehaviour
{
    // Start is called before the first frame update
    private int iceTurretCost = 15;

    public Button iceTurret;
    public void OnButtonClick()
    {
        if (GameManager.Instance.coins >= iceTurretCost)
        {
            iceTurret.gameObject.SetActive(true);
            GameManager.Instance.DecreaseCoins(iceTurretCost);
        }
        
    }
}
