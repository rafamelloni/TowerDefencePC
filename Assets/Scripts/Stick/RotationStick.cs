using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationStick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 initialPos = Vector3.zero;
    public float max = 75f;
    Vector3 dir = Vector3.zero;
    public Vector3 Dir { get { return dir; } }
    public bool shooting = false;

    void Start()
    {
        initialPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dir = Vector3.ClampMagnitude((Vector3)eventData.position - initialPos, max);
        transform.position = initialPos + dir;
        shooting = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPos;
        dir = Vector3.zero;
        shooting = false;
    }
}
