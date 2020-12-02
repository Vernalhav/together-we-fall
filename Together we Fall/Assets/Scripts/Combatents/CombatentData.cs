using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatentTypesEnum
{
    Soldier,
    Tank,
    Runner,
    Enemy,
    Construction
};

[CreateAssetMenu(fileName = "NewCombatentData", menuName = "Combatents/Combatent Data")]
public class CombatentData : ScriptableObject
{

    public float health;
    public float maxLife;
    public float attackRadius;
    public float projectileSpeed = 10;
    public float fireRate = 10;
    public float damage;

    public SpriteRenderer sr;
    public List<CombatentTypesEnum> enemiesTags;
    public GameObject bulletPrefab;
}
