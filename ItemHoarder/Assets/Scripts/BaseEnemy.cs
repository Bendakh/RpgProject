using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class BaseEnemy : ScriptableObject
{
    public string enemyName;

    public int maxHealth;
    public int attack;
    public int defense;
    public float attackSpeed;

    public int exp;
    public int currency;

    public Sprite enemySprite;

    public LootTable lootTable;

    
    //Implement an attack method here 
}
