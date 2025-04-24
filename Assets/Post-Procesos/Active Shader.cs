using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActiveShader : MonoBehaviour
{
    public Material outline;
    public Material pixels;
    public Material colorResolution;
    public Material ditherSpread;
    

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            outline.SetFloat("_color_threshold", 0.3f);
            colorResolution.SetFloat("_colorResolution", 77f);
            colorResolution.SetFloat("_ditherSpread", 0f);

            pixels.SetFloat("_pixels", 700f);



        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            pixels.SetFloat("_pixels", 900f);
            colorResolution.SetFloat("_colorResolution", 15f);
            colorResolution.SetFloat("_ditherSpread", 0.02f);
            outline.SetFloat("_color_threshold", 15f);


        }
    }
}
