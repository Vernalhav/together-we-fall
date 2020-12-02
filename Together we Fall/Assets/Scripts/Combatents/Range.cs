using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public Combatent myCombatent;
    public List<CombatentTypesEnum> enemiesTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Encontrei Inimigo:" + collision);

        foreach (CombatentTypesEnum enemy in enemiesTags)
        {
            if (collision.CompareTag(enemy.ToString()))
            {
                myCombatent.FoundEnemy(collision.gameObject.GetComponentInParent<Combatent>()); 
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        foreach (CombatentTypesEnum enemy in enemiesTags)
        {
            if (collision.CompareTag(enemy.ToString()))
            {
                myCombatent.RemoveEnemy(collision.GetComponent<Combatent>());
            }
        }
        
    }


}
