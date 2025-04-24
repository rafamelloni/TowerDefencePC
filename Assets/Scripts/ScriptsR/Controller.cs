using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public TouchManager touchManager;
    public CameraMovement cameraMove;
    // Start is called before the first frame update
    void Start()
    {
        touchManager.OnTouchHeld += HandleTouchHeld;
        touchManager.OnTouchReleased += HandleTouchReleased;
        touchManager.OnTouchDrag += HandleTouchDrag;
    }



    private void HandleTouchHeld(Vector3 touchPosition, RaycastHit hit)
    {

        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hitt = hit;

       

        if (hitt.collider)
        {
            IEPickUp pickable = hit.collider.gameObject.GetComponent<IEPickUp>();
            if (pickable != null)
            {
                
                MovableObject movableObject = hit.collider.gameObject.GetComponent<MovableObject>();
                if (movableObject != null)
                {
                    movableObject.MoveObject(touchPosition, hit);
                }
            }


        }
    }

    private void HandleTouchReleased(Vector3 touchPosition, RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hitt = hit;
        if (hitt.collider)
        {
            IEPickUp pickable = hit.collider.gameObject.GetComponent<IEPickUp>();
            if (pickable != null)
            {
                
                MovableObject movableObject = hit.collider.gameObject.GetComponent<MovableObject>();
                if (movableObject != null)
                {
                    movableObject.ReleaseObject(touchPosition, hit);
                }
            }


        }
    }
    private void HandleTouchDrag(Vector3 touchPosition, RaycastHit hit)
    {
        Vector3 inputPos = touchPosition;
        cameraMove.cameraMove(inputPos);
    }

    void OnDestroy()
    {
        touchManager.OnTouchHeld -= HandleTouchHeld;
        touchManager.OnTouchReleased -= HandleTouchReleased;
    }
}
