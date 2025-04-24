using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class activateWarining : MonoBehaviour
{
    
    public Image img;
    // Update is called once per frame
    private void Update()
    {
        if (NewEnemie.isAttack)
        {
            img.gameObject.SetActive(true);
        }
        else
        {
            img.gameObject.SetActive(false);
        }
    }
}
