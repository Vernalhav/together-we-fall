using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sniper : Enemy
{
    [SerializeField] private float shotAnimationDuration;
    [SerializeField] LineRenderer shot;
    [SerializeField] Transform firePoint;

    public override void Attack(Transform targetPosition)
    {

        PlayShootSound();

        targetPosition.GetComponent<Soldier>().ReceiveDamage(damage);

        shot.SetPosition(0, firePoint.position);
        shot.SetPosition(1, targetPosition.position);
        shot.widthMultiplier = 0.05f;
        
        if (shot != null){
            DOTween.To( () => shot.widthMultiplier,  (float x) => shot.widthMultiplier = x, 0f, shotAnimationDuration)
                    .SetEase(Ease.OutQuad);
        }
    }
}
