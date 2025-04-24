using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DronUpgrades : MonoBehaviour
{

    public RobotShooting dronFirerate;

    public RobotTargeting dronArea;

    public Image noFundsImg;

    public TMP_Text frTxt;
    public TMP_Text areaTxt;

    public GameObject frCoin;
    public GameObject areaCoin;



    public void OnButtonClickDron1()
    {
        if(GameManager.Instance.coins >= 100)
        {
            GameManager.Instance.DecreaseCoins(100);
            dronFirerate.fireRate = 0.25f;
            frTxt.text = "Max Upgraded";
            frCoin.gameObject.SetActive(false);
        }
        else
        {
            InsufficientFunds();
        }

    }
    public void OnButtonClickDron2()
    {

        if (GameManager.Instance.coins >= 100)
        {
            GameManager.Instance.DecreaseCoins(100);
            dronArea.detectionRadius = 10f;
            areaTxt.text = "Max Upgraded";
            areaCoin.gameObject.SetActive(false);
        }
        else
        {
            InsufficientFunds();
        }
    }

    public void InsufficientFunds()
    {
        noFundsImg.gameObject.SetActive(true);
        StartCoroutine(WaitForSeconds(1f));
    }

    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        noFundsImg.gameObject.SetActive(false);
    }
}
