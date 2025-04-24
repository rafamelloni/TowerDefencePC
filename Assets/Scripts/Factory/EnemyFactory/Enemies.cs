using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{


    public ObjectPool<Enemies> Pool { protected get; set; }
    public virtual void TurnOn()
    {
        gameObject.SetActive(true);
    }
    public virtual void TurnOff()
    {
        gameObject.SetActive(false);
    }


}
