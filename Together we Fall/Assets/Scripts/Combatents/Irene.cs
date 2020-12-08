using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Irene : Soldier
{    
    void Start()
    {
        health = data.maxLife;
        maxHealth = data.maxLife;
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Destination").transform;
    }
    
    private void FixedUpdate()
    {

    }


    public override void FoundEnemy(Combatent e)
    {

    }
}
