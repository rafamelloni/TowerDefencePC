using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPlace : MonoBehaviour
{
    public GameObject objectToInstantiate; // Asigna el prefab en el Inspector
    public PlaceablePlatformManager platformManager; // Asigna el PlaceablePlatformManager en el Inspector
    private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 offsetR = new Vector3(0, 1, 0);
    private GameObject instantiatedTurret;

    //normal t
    private Turrets turret;
    private Turret turrettForList;
    public TurretUpgradeManager newTurret;
    //ice t
    private segundoTurret iceT;
    public IceTurretUpgrade iceTurretList;

    private CannonShoot cannonT;
    public CannonUpgradeManager cannonList;

    public GameObject particulasDeHumpo;

    [SerializeField] AudioClip placeTurretSound;

    



    // This list stores all the turrets placed on platforms
    public List<Turrets> turretsList = new List<Turrets>();
    public void OnButtonPress()
    {
        // Get the closest platform from the PlaceablePlatformManager
        if (GameManager.Instance.coins >= 10)
        {
            GameManager.Instance.DecreaseCoins(10);
            Transform closestPlatform = platformManager.GetClosestPlatform();

            if (closestPlatform != null)
            {

                var platformScript = closestPlatform.GetComponent<PlaceablePlatform>();
                if (platformScript != null && !platformScript.isOccupied) // Check if the platform is occupied
                {

                    // Instantiate the turret at the platform's position with an offset
                    instantiatedTurret = Instantiate(objectToInstantiate, closestPlatform.position + offset, Quaternion.Euler(0, 90, 0));


                    GameObject humo = Instantiate(particulasDeHumpo, closestPlatform.position + offset, Quaternion.identity);

                    SoundFXManager.instance.PlaySoundFXClip(placeTurretSound, closestPlatform.transform, 5f);

                    Destroy(humo, 2f);

                    
                    turret = instantiatedTurret.GetComponent<Turrets>();
                    //normal t
                    turrettForList = instantiatedTurret.GetComponent<Turret>();
                  
                    newTurret.AddTurretToList(turrettForList);
                    
                    

                    //icet
                    iceT = instantiatedTurret.GetComponent<segundoTurret>();
                    iceTurretList.AddTurretToList(iceT);

                    //cannont
                    cannonT = instantiatedTurret.GetComponent<CannonShoot>();
                    cannonList.AddTurretToList(cannonT);


                    // Add the turret to the list
                    turretsList.Add(turret);

                    platformScript.Occupy(turret); // Mark the platform as occupied
                    

                }
                else
                {
                    Debug.Log("The platform is already occupied.");
                }
            }
        }
        else
        {
            GameManager.Instance.InsufficientFunds();
        }
    }

       
}
