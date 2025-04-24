using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;
    public Canvas mainCanvs;

    public float transitionTime = 3f;


    private void Start()
    {
        mainCanvs.gameObject.SetActive(false);
        Invoke(nameof(StartTutorial), 2f);
    }
    private void StartTutorial()
    {
        StartCoroutine(TutorialSequence());
    }


    private IEnumerator TutorialSequence()
    {
        // Activar la primera c�mara
        cam1.gameObject.SetActive(true);
        yield return new WaitForSeconds(transitionTime);

        // Transici�n a la segunda c�mara
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
        yield return new WaitForSeconds(transitionTime);

        // Transici�n a la tercera c�mara
        cam2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(true);
        yield return new WaitForSeconds(transitionTime);

        // Desactivar la �ltima c�mara (opcional)
        cam3.gameObject.SetActive(false);
        mainCanvs.gameObject.SetActive(true);

        // Final del tutorial
        Debug.Log("Tutorial completado");
    }
}
