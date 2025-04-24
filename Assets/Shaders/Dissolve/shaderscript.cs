using System.Collections;
using UnityEngine;

public class shaderscript : MonoBehaviour
{
    public Material originalMaterial; // Asigna el material original en el inspector
    private Material instanceMaterial; // Material de instancia
    private Coroutine dissolveCoroutine; // Referencia a la corrutina
    private Renderer rendererComponent; // Componente de Renderer

    private void Awake()
    {
        // Instancia el material y lo asigna al Renderer
        instanceMaterial = Instantiate(originalMaterial);
        rendererComponent = GetComponent<Renderer>();
        rendererComponent.material = instanceMaterial; // Asigna el material de instancia
    }

    public void Cambio()
    {
        // Si ya hay una corrutina ejecutándose, detenerla antes de iniciar una nueva
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }

        // Reiniciar el valor de _Disolve a 0
        instanceMaterial.SetFloat("_Disolve", 0f);

        // Iniciar la corrutina de disolución
        dissolveCoroutine = StartCoroutine(CambiarDissolve());
    }

    private IEnumerator CambiarDissolve()
    {
        float tiempo = 2f; // Tiempo total en segundos
        float tiempoTranscurrido = 0f;
        float valorInicial = 0f; // Inicialmente visible
        float valorFinal = 1f; // Finalmente invisible

        while (tiempoTranscurrido < tiempo)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / tiempo;
            instanceMaterial.SetFloat("_Disolve", Mathf.Lerp(valorInicial, valorFinal, t));
            yield return null; // Espera el siguiente frame
        }

        // Asegura que termine en 1
        instanceMaterial.SetFloat("_Disolve", valorFinal);
    }

    // Método para resetear el material cuando el enemigo respawnea o es destruido
    public void ResetMaterial()
    {
        // Detener cualquier corrutina activa
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }

        // Resetear el valor de _Disolve a 0
        instanceMaterial.SetFloat("_Disolve", 0f);
        dissolveCoroutine = null; // Eliminar la referencia a la corrutina
    }

    private void OnDestroy()
    {
        // Detener la corrutina cuando el enemigo es destruido
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }
    }
}
