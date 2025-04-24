using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEPickUp
{
    public void OnPickUp(Vector3 position);
    public void OnPickDown(Vector3 position);
}

