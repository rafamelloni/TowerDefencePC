using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunShoot : MonoBehaviour
{
    [SerializeField] RotationStick rotStick; // Joystick para rotación
    [SerializeField] private Weapon _weapon;

    private float fireRate = 0.2f; // Intervalo en segundos entre disparos
    private float lastShotTime = 0f; // Tiempo del último disparo
    // Start is called before the first frame update
    void Start()
    {
        print("sfafasfa");
        Func();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotStick.Dir != Vector3.zero)
        {
           
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(rotStick.Dir.x, 0, rotStick.Dir.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20f); // Ajusta la velocidad de rotación
            Shoot();
        }
    }

    void Func()
    {
        this.gameObject.SetActive(true);
    }
    public void Shoot()
    {
        if (_weapon != null && Time.time >= lastShotTime + fireRate)
        {
            _weapon.Shoot();
            lastShotTime = Time.time; // Actualiza el tiempo del último disparo
        }

    }
}
