using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{

    public override void Attack(Transform targetPosition)
    {
        Debug.Log("POOWWW!");
        targetPosition.GetComponent<Soldier>().ReceiveDamage(damage);
    }
}
