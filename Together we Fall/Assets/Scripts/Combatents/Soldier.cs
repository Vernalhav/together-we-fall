﻿using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Soldier : Combatent
{
    [SerializeField] private AIPath myPath; 

    [SerializeField] private List<AudioSource> walkSounds;

    private AudioSource walkSound;
    
    private float time = 0;
    
    [ContextMenu("Refresh prefab")]
    private void Awake() {
        range = GetComponentInChildren<Range>();
        health = data.maxLife;
        maxHealth = data.maxLife;
        attackRadius = data.attackRadius;
        projectileSpeed = data.projectileSpeed;
        fireRate = data.fireRate;
        damage = data.damage;
        bulletPrefab = data.bulletPrefab;
        range.enemiesTags = data.enemiesTags;
        GetComponent<AIPath>().maxSpeed = data.moveSpeed + UnityEngine.Random.Range(-0.2f, 0.2f);

        range.GetComponent<CircleCollider2D>().radius = attackRadius;
    }

    void Start()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Destination").transform;
        PlayRandomWalk();
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("Animator is null!");
        animator?.SetBool("Moving", true);
    }

    private void PlayRandomWalk()
    {
        if(walkSounds.Count > 0){
            walkSound = walkSounds[UnityEngine.Random.Range(0, walkSounds.Count)];
            walkSound.Play();
        }
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
            animator.SetBool("Moving", true);
            PlayRandomWalk();
        }
    }
    
    public override void FoundEnemy(Combatent e)
    {
        myPath.enabled = false;
        if(animator != null)
            animator.SetBool("Moving", false);
        if(walkSound != null)
            walkSound.Stop();
        enemiesList.Add(e);
    }

    protected override void Death()
    {
        TroopsTracker.OnTroopDied(data.myType);

        PlayDeathSounds();

        if(animator!=null)
            animator.SetTrigger("Died");        
        
        Collider2D collider = GetComponent<Collider2D>();
        enabled = false;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if(sr != null){
            sr.sortingOrder = 0;
        }

        if(collider != null){
            collider.enabled = false;
        }
        
        AIPath aiPath = GetComponent<AIPath>();

        if(aiPath != null){
            aiPath.enabled = false;
        }
    }

}
