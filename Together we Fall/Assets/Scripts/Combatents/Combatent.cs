using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Combatent: MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<Combatent> enemiesList;
    protected SpriteRenderer sr;
    protected Range range;
     
    protected float health;
    protected float maxHealth;
    protected float attackRadius;
    protected float projectileSpeed = 10;
    protected float fireRate = 10;
    protected float damage;


    public virtual void Attack(Transform enemy)
    {
        if (enemy == null) return;

        GameObject _b = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation) as GameObject;
        
        _b.GetComponent<Bullet>().Shoot(enemy.transform, projectileSpeed, damage, gameObject.tag);
    }

    public virtual void ReceiveDamage(float dmg)
    {
        health = System.Math.Max(0, health - dmg);
        if (health <= 0) Death();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual void FoundEnemy(Combatent e)
    {
        enemiesList.Add(e);
    }

    public virtual Combatent NextEnemy()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            Combatent e = enemiesList[i];
            
            if (e == null)
            {
                enemiesList.RemoveAt(i);
            }
            else
            {
                return e;
            }
        }


        return null;
    }

    internal void RemoveEnemy(Combatent cb)
    {
        enemiesList.Remove(cb);
    }

}
