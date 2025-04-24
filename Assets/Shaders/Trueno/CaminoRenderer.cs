using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminoRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] controlPoints; // Asigna los 21 puntos como objetos vacíos en el Inspector.
    public int segmentCount = 20; // Número de segmentos por cada par de puntos.

    void Start()
    {
        DrawCurve();
    }

    void DrawCurve()
    {
        if (controlPoints.Length < 2)
        {
            Debug.LogError("Se necesitan al menos 2 puntos de control para una curva.");
            return;
        }

        List<Vector3> curvePoints = new List<Vector3>();

        for (int i = 0; i < controlPoints.Length - 1; i++)
        {
            // Obtener los puntos necesarios para Catmull-Rom
            Vector3 p0 = i == 0 ? controlPoints[i].position : controlPoints[i - 1].position;
            Vector3 p1 = controlPoints[i].position;
            Vector3 p2 = controlPoints[i + 1].position;
            Vector3 p3 = i + 2 < controlPoints.Length ? controlPoints[i + 2].position : controlPoints[i + 1].position;

            // Generar segmentos entre p1 y p2
            for (int j = 0; j <= segmentCount; j++)
            {
                float t = j / (float)segmentCount;
                Vector3 point = CalculateCatmullRomPoint(t, p0, p1, p2, p3);
                curvePoints.Add(point);
            }
        }

        // Configurar el Line Renderer
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    Vector3 CalculateCatmullRomPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Fórmula de Catmull-Rom
        float t2 = t * t;
        float t3 = t2 * t;

        Vector3 point = 0.5f * (
            2f * p1 +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );

        return point;
    }
}
