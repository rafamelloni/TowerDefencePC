using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class NormalBullet : Bullets
{
    [SerializeField] float _duration;
    float _counter;
    bool collisiono = false;
    public float dmg = 1f;

    public GameObject Fire;
    public GameObject Smoke;
    public GameObject Sparks;
    public GameObject Flash;

    public ParticlePool pool;

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _counter += Time.deltaTime;
        if (_counter >= _duration || collisiono)
        {
            Pool.Return(this);
        }
    }

    public override void TurnOn()
    {
        base.TurnOn();
        pool = FindObjectOfType<ParticlePool>();
        _counter = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 collisionPoint = other.ClosestPointOnBounds(transform.position);


            // Instanciar las partículas en la posición de colisión

            if (pool != null)
            {
                pool.GetParticle(Sparks, collisionPoint);
                pool.GetParticle(Smoke, collisionPoint);
                pool.GetParticle(Fire, collisionPoint);
                pool.GetParticle(Flash, collisionPoint);
            }

            

            Pool.Return(this);
            other.gameObject.GetComponent<Enemie>().TakeDmg(dmg);

            
        }

        if (other.gameObject.CompareTag("FinalBoss"))
        {
            Vector3 collisionPoint = other.ClosestPointOnBounds(transform.position);


            if (pool != null)
            {
                pool.GetParticle(Sparks, collisionPoint);
                pool.GetParticle(Smoke, collisionPoint);
                pool.GetParticle(Fire, collisionPoint);
                pool.GetParticle(Flash, collisionPoint);
            }

            Pool.Return(this);
            other.gameObject.GetComponent<FinalBossBehaviour>().TakeDamage(dmg);


        }
    }
}
