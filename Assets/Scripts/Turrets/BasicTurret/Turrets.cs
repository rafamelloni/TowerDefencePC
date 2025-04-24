
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

 

    // M�todo virtual para que cada torreta defina qu� hacer en cada mejora
    


}
