using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    [SerializeField] protected float _speed;
    public ObjectPool<Bullets> Pool { protected get; set; }
    public  virtual void TurnOn()
    {
        gameObject.SetActive(true);
    }
    public  virtual void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
