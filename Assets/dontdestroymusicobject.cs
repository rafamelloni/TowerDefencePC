using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroymusicobject : MonoBehaviour
{
    public static dontdestroymusicobject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
