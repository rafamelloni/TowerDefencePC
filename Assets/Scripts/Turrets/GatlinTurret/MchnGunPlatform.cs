using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MchnGunPlatform : MonoBehaviour
{
    public Transform Player;
    public Canvas machinegunCanvas;
    
    public bool canvaActive = false;
    void Start()
    {
        machinegunCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance( transform.position, Player.transform.position) < 5)
        {
            if(!canvaActive)
            machinegunCanvas.gameObject.SetActive(true);
            canvaActive = true;


        }
        else{
            machinegunCanvas.gameObject.SetActive(false);
            canvaActive = false;
        }
    }
}
