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
        health = data.maxLife;
        maxHealth = data.maxLife;
        attackRadius = data.attackRadius;
        projectileSpeed = data.projectileSpeed;
        fireRate = data.fireRate;
        damage = data.damage;
        bulletPrefab = data.bulletPrefab;
        sr = GetComponent<SpriteRenderer>();

        range.enemiesTags = data.enemiesTags;
        range.GetComponent<CircleCollider2D>().radius = attackRadius;
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Destination").transform;
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
    public override void FoundEnemy(Combatent e)
    {
        myPath.enabled = false;
        enemiesList.Add(e);
    }
}
