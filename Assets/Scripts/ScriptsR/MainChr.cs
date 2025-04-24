using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChr : MonoBehaviour
{
    [SerializeField] Stick stick;          // Joystick para movimiento
   

    void Update()
    {
        // Movimiento
        Vector3 moveDir = new Vector3(stick.Dir.x, 0, stick.Dir.y);
        transform.position += moveDir * RemoteConfigHandler.instance.Speed * Time.deltaTime;

        // Rotación
      
    }
}
