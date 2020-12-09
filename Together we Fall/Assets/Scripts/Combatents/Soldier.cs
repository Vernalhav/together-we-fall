using Pathfinding;
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

        range.enemiesTags = data.enemiesTags;
        range.GetComponent<CircleCollider2D>().radius = attackRadius;
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Destination").transform;
        PlayRandomWalk();
        animator = GetComponent<Animator>();
        animator.SetBool("Moving", true);
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
        animator.SetBool("Moving", false);
        if(walkSound != null)
            walkSound.Stop();
        enemiesList.Add(e);
    }
}
