using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<BaseEnemy> enemiesList = new List<BaseEnemy>();


    //Make an attack method in base enemy
    public BaseEnemy currentEnemy;
    [SerializeField]
    Player player;

    
    [SerializeField]
    public int currentHealth;

    private float attackSpeedCounter;

    private void Awake()
    {
        /*if (currentEnemy != null)
        {
            currentHealth = currentEnemy.maxHealth;
            attackSpeedCounter = currentEnemy.attackSpeed;
        }*/
        
    }
    private void Start()
    {
        SpawnEnemy();
    }
    public int GetDefense()
    {
        return (int)this.currentEnemy.defense;
    }

    private void Update()
    {
        if (currentEnemy != null)
        {
            attackSpeedCounter -= Time.deltaTime;
            if (attackSpeedCounter <= 0)
            {
                //Attack();
                attackSpeedCounter = currentEnemy.attackSpeed;
            }
        }
    }

    public void Attack()
    {
        int dmg = CalculateDamage((int) player.armor.Value);
        player.SendMessage("TakeDamage", dmg);
    }

    public int CalculateDamage(int playerArmor)
    {
        return Mathf.FloorToInt(((float)currentEnemy.attack * (1 + Random.Range(-/*attackModifier*/0.2f, /*attackModifier*/0.2f))) * (100 / (100 + (float)playerArmor)));
    }

    /*public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        DamageNumberGenerator._instance.InstantiateDamageNumber(dmg, transform.position);
        if (currentHealth <= 0)
        {
            Die();
        }
    }*/

    public void TakeDamage(int dmg, bool isCriticalDmg)
    {
        currentHealth -= dmg;

        if (isCriticalDmg)
            DamageNumberGenerator._instance.InstantiateDamageNumber(dmg, transform.position, Color.yellow, TMPro.FontStyles.Bold);
        else
            DamageNumberGenerator._instance.InstantiateDamageNumber(dmg, transform.position, Color.red, TMPro.FontStyles.Normal);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        GameManager._instance.player.GetExp(currentEnemy.exp);
        GameManager._instance.player.GetCurrency(currentEnemy.currency);
        GameManager._instance.EndCombat();
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int enemyToSpawnIndex = Random.Range(0, enemiesList.Capacity);
        InitializeEnemy(enemiesList[enemyToSpawnIndex]);
    }

    private void InitializeEnemy(BaseEnemy enemyToInstantiate)
    {
        currentEnemy = enemyToInstantiate;
        currentHealth = currentEnemy.maxHealth;
        attackSpeedCounter = currentEnemy.attackSpeed;
        GetComponent<SpriteRenderer>().sprite = currentEnemy.enemySprite;
    }
}
