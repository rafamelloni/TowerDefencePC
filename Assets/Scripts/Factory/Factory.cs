using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T> : MonoBehaviour
{
    public abstract T Create();

}
