using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatent
{

    protected float time; //used in time calculation during attack

    public void Start()
    {
        range = GetComponentInChildren<Range>();
        health = data.maxLife;
        maxHealth = data.maxLife;
        attackRadius = data.attackRadius;
        projectileSpeed = data.projectileSpeed;
        fireRate = data.fireRate;
        damage = data.damage;
        bulletPrefab = data.bulletPrefab;
 
        range.enemiesTags = data.enemiesTags;
        range.GetComponent<CircleCollider2D>().radius = attackRadius;

        Physics.IgnoreLayerCollision(0, 0);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (enemiesList.Count == 0) return;

        Combatent _actTarget = NextEnemy();

        if (_actTarget == null) return;

        float _distance = Vector2.Distance(transform.position, _actTarget.transform.position);

        if (_distance <= attackRadius && time >= 1 / fireRate)
        {
            PlayShootSound();
            Attack(_actTarget.transform);
            time = 0;
        }
    }
}
