using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TouchManager : MonoBehaviour
{
    public float rayLength = 100f;

    public Camera mainCamera;
    public Vector3 CurrentInputPosition { get; private set; }

    public System.Action<Vector3, RaycastHit> OnTouchHeld;
    public System.Action<Vector3, RaycastHit> OnTouchReleased;
    public System.Action<Vector3, RaycastHit> OnTouchDrag;

    void Update()
    {


        // Verificar toques en pantalla táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // Solo considerar el toque si está siendo mantenido o se ha levantado
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    Vector3 inputPosition = hit.point;
                    if (hit.collider.CompareTag("Shop"))
                    {
                        //OnTouchDrag?.Invoke(inputPosition, hit);
                    }
                    else 
                    {
                        OnTouchHeld?.Invoke(inputPosition, hit);
                    } 
                }
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    Vector3 inputPosition = hit.point;

                    OnTouchReleased?.Invoke(inputPosition, hit);
                }
            }

        }
    }
}

