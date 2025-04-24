using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHealth : MonoBehaviour
{
    public float fireRate = 1f;  // Tiempo entre cada disparo (1 segundo en este caso)
    private float timer = 0f;

    public HealtBarTurret healthBar;  // Referencia a la barra de salud
    private float currentHealth;
    private float maxHealth = 100f;

    public CannonTurret cannonT;

    public PlaceablePlatform platform;




    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void Update()
    {
        RecibeDmg();
    }


    public void RecibeDmg()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.3f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy2"))
            {

                if (timer <= 0f)
                {
                    print("tomo dmg");
                    ApplyDamage(10f);

                    timer = fireRate;

                }
            }
        }
        timer -= Time.deltaTime;
    }

    public void ApplyDamage(float damageAmount)
    {

        currentHealth -= damageAmount;


        currentHealth = Mathf.Max(currentHealth, 0f);


        healthBar.UpdateHealthBar(maxHealth, currentHealth);


        if (currentHealth <= 0f)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("La torreta ha muerto.");

        cannonT.Die();


    }
}
