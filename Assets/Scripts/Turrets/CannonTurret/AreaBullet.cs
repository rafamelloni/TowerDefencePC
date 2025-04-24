using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBullet : MonoBehaviour
{
    public float speed = 150f;
    public float explosionRadius = 5f;
    public float damage = 2f;
    
    public LayerMask enemyLayer;

    private Transform target;

    public ParticlePool pool;
    public GameObject explosionEffect;
    public AudioClip fireSoundClip;

    private void Awake()
    {
        pool = FindObjectOfType<ParticlePool>();
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explotar();
            print("coll");
        }
        if (other.gameObject.CompareTag("FinalBoss"))
        {
            Explotar1();
        }
      
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Explotar()
    {
  
       pool.GetParticle(explosionEffect, transform.position);
        SoundFXManager.instance.PlaySoundFXClip(fireSoundClip, transform, 0.5f);


        Collider[] enemigos = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider enemigo in enemigos)
        {
            
            Enemie enemigoScript = enemigo.GetComponent<Enemie>();
            if (enemigoScript != null)
            {
                enemigoScript.TakeDmg(damage);
            }
        }

       
    }

    void Explotar1()
    {

        pool.GetParticle(explosionEffect, transform.position);
        SoundFXManager.instance.PlaySoundFXClip(fireSoundClip, transform, 0.5f);


        Collider[] enemigos = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider enemigo in enemigos)
        {

            FinalBossBehaviour BOSS = enemigo.GetComponent<FinalBossBehaviour>();
            if (BOSS != null)
            {
                BOSS.TakeDamage(damage);
            }
         
        }


    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
