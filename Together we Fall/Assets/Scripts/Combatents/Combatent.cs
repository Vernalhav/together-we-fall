using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Combatent: MonoBehaviour
{
    [SerializeField] protected List<AudioSource> deathSounds;
    [SerializeField] protected List<AudioSource> shootSounds;
    
    public CombatentData data;
    public GameObject bulletPrefab;
    protected List<Combatent> enemiesList= new List<Combatent>();
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

        PlayShootSound();

        GameObject _b = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation) as GameObject;

        _b.GetComponent<Bullet>().Shoot(enemy.transform, projectileSpeed, damage, gameObject.tag);
    }

    protected void PlayShootSound()
    {
        if (shootSounds.Count > 0)
            shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)].Play();
    }

    public virtual void ReceiveDamage(float dmg)
    {
        health = System.Math.Max(0, health - dmg);
        if (health <= 0) Death();
    }

    protected virtual void Death()
    {
        PlayDeathSounds();
        TroopsTracker.OnTroopDied(data.myType);
        Destroy(gameObject);
    }

    protected void PlayDeathSounds()
    {
        if (deathSounds.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, deathSounds.Count);
            AudioSource.PlayClipAtPoint(deathSounds[rand].clip, transform.position);
        }
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
