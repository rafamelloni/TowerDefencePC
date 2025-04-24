using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
    [SerializeField] GameObject sparks;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 es el botón izquierdo del mouse
        {
            ClickOnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseMenu.instance.OnButtonPress();
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                PauseMenu.instance.OnButtonPress2();
            }
        }
    }

    void ClickOnEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy2"))
            {

                Vector3 collisionPoint = hit.collider.ClosestPointOnBounds(transform.position);

                float offsetY = 2f;  // Ajusta este valor según lo necesites
                Vector3 spawnPosition = collisionPoint + new Vector3(0, offsetY, 0);


                Debug.Log("Hiciste clic en un enemigo con el tag Enemy2: " + hit.collider.name);
                BaseNewEnemy newEnemie = hit.collider.GetComponent<BaseNewEnemy>();
                newEnemie.TakeDmg(1);

                GameObject sparkInstance = Instantiate(sparks, spawnPosition, Quaternion.identity);
                Destroy(sparkInstance, 2f); // Destruye el objeto después de 2 segundos
            }
        }
    }
}
