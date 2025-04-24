
using UnityEngine;


public enum TurretType
{
    NormalTurret,
    Machinegun,
    SlowTurret,
    Cannon

}
public abstract class Turrets : MonoBehaviour
{

  
    public PlaceablePlatform platform;
    public abstract void Shoot();
    public TurretType type;
    public abstract void Die();

 

    // Método virtual para que cada torreta defina qué hacer en cada mejora
    


}
