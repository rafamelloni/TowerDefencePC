using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNewEnemy : MonoBehaviour
{
    public ObjectPool<BaseNewEnemy> Pool { protected get; set; }

    private float currentLife = 3f;

    public NewEnemie newE;
    private List<Turret> turrets = new List<Turret>();


    private void Start()
    {
        newE = GetComponentInChildren<NewEnemie>();
    }
    public void SetTargets( Transform player)
    {
        
        newE._player = player;
    }

    public virtual void TurnOn()
    {
        gameObject.SetActive(true);
    }
    public virtual void TurnOff()
    {
        gameObject.SetActive(false);
    }
    public void TakeDmg(float dmg)
    {
        currentLife -= dmg;

        if (currentLife <= 0)
        {
            //Death();
            Death();
        }
    }
    void Death()
    {
        GameManager.Instance.IncreaseCoins(17);
        Pool.Return(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            GameManager.Instance.LoseLife();
            Pool.Return(this);
        }
    }


}


