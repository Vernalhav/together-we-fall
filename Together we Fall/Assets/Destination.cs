using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public CombatentTypesEnum[] tags;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        foreach(CombatentTypesEnum c in tags){
            if(collision.tag == c.ToString()){

                if(collision.tag == "Irene"){
                    TroopsTracker.OnIreneFinished();
                }

                TroopsTracker.OnTroopFinished();
                collision.gameObject.SetActive(false);
                return;
            }
        }
    }
}
