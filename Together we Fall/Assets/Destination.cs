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
                TroopsTracker.OnTroopFinished();
                collision.gameObject.SetActive(false);
                return;
            }if(collision.tag == "Irene"){
                collision.gameObject.SetActive(false);
                TroopsTracker.OnIreneFinished();
                return;
            }
        }

    }

    

}
