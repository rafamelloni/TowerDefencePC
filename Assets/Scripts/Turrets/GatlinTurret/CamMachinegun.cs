using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CamMachinegun : MonoBehaviour
{
 
    public Canvas canvas;
    public Canvas canvasOriginalPlace;
    public Canvas mainCanvasPlayer;


    public GameObject Player;
   
    public Image joystick;

    void Start()
    {
        gameObject.SetActive(false);  // Desactivar el GameObject también
    }

    public void PresionoBotonEnter()
    {
        gameObject.SetActive(true);  // Desactivar el GameObject también
        canvasOriginalPlace.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
       // Player.gameObject.SetActive(false);
        //mainCanvasPlayer.gameObject.SetActive(false);

    }
    public void PresionoBotonExit()
    {

        gameObject.SetActive(false);  // Desactivar el GameObject también
        canvas.gameObject.SetActive(false);
        canvasOriginalPlace.gameObject.SetActive(true);
        mainCanvasPlayer.gameObject.SetActive(true);
        joystick.gameObject.SetActive(false);
        //Player.gameObject.SetActive(true);

    }


    private void Update()
    {
        if (gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(true);
           
        }

    }
}
