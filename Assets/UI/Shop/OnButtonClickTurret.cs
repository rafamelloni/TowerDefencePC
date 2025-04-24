using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonClickTurret : MonoBehaviour
{
    public GameObject turretCanvas;

    public void OnButtonClick()
    {
        turretCanvas.SetActive(true);
    }
}
