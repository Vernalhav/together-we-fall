using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatentTypesEnum
{
    Soldier,
    Tank,
    Runner,
    Enemy,
    Irene,
    Construction
};

[CreateAssetMenu(fileName = "NewCombatentData", menuName = "Combatents/Combatent Data")]
public class CombatentData : ScriptableObject
{
    public CombatentTypesEnum myType;
    public float maxLife;
    public float attackRadius;
    public float projectileSpeed = 10;
    public float fireRate = 10;
    public float damage;

    public List<CombatentTypesEnum> enemiesTags;
    public GameObject bulletPrefab;
}
