using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectShop : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Button botonShop;
    public GameObject luzShop;
    void Start()
    {
        botonShop.gameObject.SetActive(false);
        luzShop.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 15)
        {
            botonShop.gameObject.SetActive(true);
            luzShop.gameObject.SetActive(true);

        }
        else
        {
            botonShop.gameObject.SetActive(false);
            luzShop.gameObject.SetActive(false);



        }
    }
}
