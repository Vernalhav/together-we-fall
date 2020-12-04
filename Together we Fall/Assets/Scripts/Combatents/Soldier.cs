using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Soldier : Combatent
{
    [SerializeField] private AIPath myPath;
  
    private float time = 0;
       
    void Start()
    {
        range = GetComponentInChildren<Range>();
        health = data.health;
        maxHealth = data.maxLife;
        attackRadius = data.attackRadius;
        projectileSpeed = data.projectileSpeed;
        fireRate = data.fireRate;
        damage = data.damage;
        bulletPrefab = data.bulletPrefab;
        sr = GetComponent<SpriteRenderer>();

        range.enemiesTags = data.enemiesTags;
        range.GetComponent<CircleCollider2D>().radius = attackRadius;
        //sr.color = new Color(0, 1, 0, 1)
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Destination").transform;
        Debug.Log(GetComponent<AIDestinationSetter>().target);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (enemiesList.Count > 0 && time >= 1 / fireRate)
        {
            time = 0;
            Combatent target = NextEnemy();
            if (target != null){
                Attack(target.transform);
            }
        } else if (enemiesList.Count <= 0 && myPath.enabled == false)
        {
            myPath.enabled = true;
            //Debug.Log("Habilitei o path! " + enemiesList.Count);
        }
    }

    public override void ReceiveDamage(float _dmg)
    {
        base.ReceiveDamage(_dmg);
       //UpdateHealthColor();
    }

    public override void FoundEnemy(Combatent e)
    {
        myPath.enabled = false;
        enemiesList.Add(e);   
        
    }   

    private void UpdateHealthColor()
    {
        float _healthPercentage = health / maxHealth;
        if( 0.6f < _healthPercentage && _healthPercentage <= 1)
        {
            sr.color = new Color(0, 1, 0, 1);
        } else if (0.3 < _healthPercentage && _healthPercentage <= 0.6)
        {
            sr.color = new Color(1, 1, 1);
        }
        else
        {
            sr.color = new Color(1, 0, 1);
        }

    }    
}
