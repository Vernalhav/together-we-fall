using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : Combatent
{
    [SerializeField] private LayerMask layerToAttack;
    [SerializeField] private float explosionRadius = 0;

    [SerializeField] private Animator myAnimator;
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer mySr;


    void Start()
    {
        myAnimator = GetComponent<Animator>();

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
    }

    public override void ReceiveDamage(float _dmg)
    {
        base.ReceiveDamage(_dmg);
        //UpdateHealthColor();
    }

    public override void FoundEnemy(Combatent e)
    {
        Explode();
    }

    private void Explode()
    {
        mySr.enabled = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerToAttack);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<Combatent>().ReceiveDamage(damage);
        }
        myAnimator.SetTrigger("Explode");
    }
}
