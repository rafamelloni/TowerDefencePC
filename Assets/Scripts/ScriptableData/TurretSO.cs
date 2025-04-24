using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTurretDefault", menuName = "ScriptableObjects/Data/Turrets")]
public class TurretSO : ScriptableObject
{
    
    public float dist ; // Distancia mínima para disparar
    public float rotationDistance; // Distancia para rotar hacia el enemigo
    public float fireRate ; // Tiempo entre disparos en segundos
    public float rotationSpeed;

}
