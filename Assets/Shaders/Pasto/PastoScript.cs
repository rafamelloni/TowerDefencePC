using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastoScript : MonoBehaviour
{
    public GameObject Player;
    Material grassMaterial;
    void Start()
    {
        grassMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector3 playerposition = Player.GetComponent<Transform>().position;
        grassMaterial.SetVector("referencia", playerposition);
    }
}
