using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : Enemy
{
    [SerializeField] private LayerMask layerToAttack;
    [SerializeField] private float explosionRadius = 0;

    [SerializeField] private Animator myAnimator;
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer mySr;


    void Start()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
    }

    public override void ReceiveDamage(float _dmg)
    {
        base.ReceiveDamage(_dmg);
    }

    public override void FoundEnemy(Combatent e)
    {
        Explode();
    }

    private void Explode()
    {
        PlayShootSound();

        mySr.enabled = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerToAttack);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Combatent>().ReceiveDamage(damage);
        }
        
        myAnimator.SetTrigger("Explode");
        
        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake());
    }
}
