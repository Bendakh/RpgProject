using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool canAttack = false;

    /*
     *  Currency earned (percentage)
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */

    public Stat maxHealth;
    public Stat maxMana;
    public Stat damage;
    public Stat armor;
    public Stat attackSpeed;
    //Capped at 65 maybe
    public Stat critChance;

    public int currentHealth { get; private set; }
    public int currentMana { get; private set; }

    private int level = 1;
    private int currency;
    private int currentXp;
    private int toReachXp;
    private int attributePoints = 5;

    public int AttributePoints { get => attributePoints; set => attributePoints = value; }
    public int Currency { get => currency; }
    public int CurrentXp { get => currentXp; }

    public int Level { get => level; }

    public int ToReachXp { get => toReachXp; }

    private float attackSpeedCounter;

    private int hpBonus = 0;
    private StatModifier hpBonusModifier;

    private int damageBonus = 0;
    private StatModifier damageBonusModifier;

    private int armorBonus = 0;
    private StatModifier armorBonusModifier;

    private int attackSpeedBonus = 0;
    private StatModifier attackSpeedBonusModifier;

    private int critChanceBonus = 0;
    private StatModifier critChanceBonusModifier;

    public int HpBonus { get => hpBonus; }
    public int DamageBonus { get => damageBonus; }
    public int ArmorBonus { get => armorBonus; }
    public int AttackSpeedBonus { get => attackSpeedBonus; }
    public int CritChanceBonus { get => critChanceBonus; }

    public Enemy target;

    [Range(0f, 0.3f)]
    public float attackModifier;

    [SerializeField]
    Inventory inventory;

    public Inventory PlayerInventory { get => inventory; }

    private void Awake()
    {
        if (GameManager._instance.isLoaded)
        {
            InitializePlayerData(GameManager._instance.mockData);
        }
        else
        {
            maxHealth = new Stat(20f);
            maxMana = new Stat(5f);
            damage = new Stat(5f);
            armor = new Stat(3f);
            attackSpeed = new Stat(5f);
            critChance = new Stat(0f);

            attackSpeedCounter = attackSpeed.Value;
            currentHealth = (int)maxHealth.Value;

            hpBonusModifier = new StatModifier(hpBonus, 0, StatModType.FLAT, TargetStat.HEALTH);
            damageBonusModifier = new StatModifier(damageBonus, 0, StatModType.FLAT, TargetStat.ATTACK);
            armorBonusModifier = new StatModifier(armorBonus, 0, StatModType.FLAT, TargetStat.DEFENSE);
            attackSpeedBonusModifier = new StatModifier(attackSpeedBonus, 0, StatModType.FLAT, TargetStat.ATTACKSPEED);
            critChanceBonusModifier = new StatModifier(critChanceBonus, 0, StatModType.FULLPERCENT, TargetStat.CRITCHANCE);

            maxHealth.AddModifier(hpBonusModifier);
            damage.AddModifier(damageBonusModifier);
            armor.AddModifier(armorBonusModifier);
            attackSpeed.AddModifier(attackSpeedBonusModifier);
            critChance.AddModifier(critChanceBonusModifier);
        }
    }

    private void Start()
    {
        CalculateNewToReachXp();
    }

    private void Update()
    {
        if (GameManager._instance.IsFighting)
        {
            attackSpeedCounter -= Time.deltaTime;
            if (attackSpeedCounter <= 0)
            {
                Attack(target);
                attackSpeedCounter = attackSpeed.Value;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void GetExp(int xp)
    {
        this.currentXp += xp;
        if(currentXp >= toReachXp)
        {
            LevelUp();
        }
    }

    public void GetCurrency(int cur)
    {
        this.currency += cur;
    }

    private void LevelUp()
    {
        level++;
        attributePoints += 2;
        CalculateNewToReachXp();
    }

    private void CalculateNewToReachXp()
    {
        currentXp = 0;
        toReachXp = (100 * level * level * level) / 2;
    }

    private void Attack(Enemy target)
    {
        int dmg = 0;
        bool isCritical = Random.Range(0f, 100f) <= this.critChance.Value;

        if (!isCritical)
            dmg = CalculateDamage(target.GetDefense());
        else
            dmg = CalculateCriticalDamage(target.GetDefense());

        target.TakeDamage(dmg, isCritical);
    }

    private int CalculateDamage(int enemyDefense)
    {
        return Mathf.FloorToInt(((float)damage.Value * (1 + Random.Range(-attackModifier, attackModifier))) * (100 / (100 + (float)enemyDefense)));
    }

    private int CalculateCriticalDamage(int enemyDefense)
    {
        // We can add critical damage one day
        return Mathf.FloorToInt((((float)damage.Value * (1 + Random.Range(-attackModifier, attackModifier))) * 1.5f) * (100 / (100 + (float) enemyDefense)));
    }

    private void Die()
    {
        //TODO
    }

    public void ApplyBonuses(int hp, int damage, int armor, int attackSpeed, int critChance)
    {
        //1 point = 1 hp
        hpBonus += hp;
        //2 points = 1 damage
        damageBonus += damage;
        //3 points = 1 armor
        armorBonus += armor;
        //1 point = 0.05 attackspeed
        attackSpeedBonus += attackSpeed;
        //1 point = 0.05 chance
        critChanceBonus += critChance;

        this.hpBonusModifier.value = hpBonus;
        this.maxHealth.SetDirty();

        this.damageBonusModifier.value = (float) damageBonus / 2;
        this.damage.SetDirty();

        this.armorBonusModifier.value = (float) armorBonus / 3;
        this.armor.SetDirty();

        this.attackSpeedBonusModifier.value = (float) attackSpeedBonus * -0.1f;
        this.attackSpeed.SetDirty();

        this.critChanceBonusModifier.value = (float) critChanceBonus * 0.05f;
        this.critChance.SetDirty();
    }

    public float GetAttackCounter()
    {
        return this.attackSpeedCounter;
    }

    public void AddItemModifiers(GeneratedItem item)
    {
        foreach(StatModifier sm in item.modifiers)
        {
            switch(sm.targetStat)
            {
                case TargetStat.HEALTH:
                    sm.source = item;
                    maxHealth.AddModifier(sm);
                    //Update UI
                    Debug.Log("New health value " + maxHealth.Value);
                    break;
                case TargetStat.DEFENSE:
                    sm.source = item;
                    armor.AddModifier(sm);
                    Debug.Log("New defense value " + armor.Value);
                    break;
                case TargetStat.ATTACK:
                    sm.source = item;
                    damage.AddModifier(sm);
                    Debug.Log("New damage value " + damage.Value);
                    break;
                case TargetStat.ATTACKSPEED:
                    sm.source = item;
                    attackSpeed.AddModifier(sm);
                    Debug.Log("New attackspeed value " + attackSpeed.Value);
                    break;
                case TargetStat.CRITCHANCE:
                    sm.source = item;
                    critChance.AddModifier(sm);
                    Debug.Log("New critical chance value " + critChance.Value);
                    break;
            }
        }
    }

    

    public void RemoveItemModifiers(GeneratedItem item)
    {
        if(maxHealth.RemoveAllModifiersFromSource(item))
            Debug.Log("New health value " + maxHealth.Value);
        if (armor.RemoveAllModifiersFromSource(item))
            Debug.Log("New defense value " + armor.Value);
        if (damage.RemoveAllModifiersFromSource(item))
            Debug.Log("New damage value " + damage.Value);
        if(attackSpeed.RemoveAllModifiersFromSource(item))
            Debug.Log("New attackspeed value " + attackSpeed.Value);
        if (critChance.RemoveAllModifiersFromSource(item))
            Debug.Log("New critical chance value " + critChance.Value);
    }







    public void InitializePlayerData(PlayerSaveData playerSaveData)
    {
        this.level = playerSaveData.level;
        this.currency = playerSaveData.currency;
        this.currentXp = playerSaveData.currentExp;
        this.toReachXp = playerSaveData.toReachXp;
        this.attributePoints = playerSaveData.attributePoints;

        this.currentHealth = playerSaveData.currentHealth;
        this.currentMana = playerSaveData.currentMana;

        this.hpBonus = playerSaveData.hpBonus;
        this.damageBonus = playerSaveData.damageBonus;
        this.armorBonus = playerSaveData.armorBonus;
        this.attackSpeedBonus = playerSaveData.attackSpeedBonus;
        this.critChanceBonus = playerSaveData.critChanceBonus;

        foreach(ItemSaveData savedItem in playerSaveData.inventoryItems)
        {
            List<StatModifier> modifiers = new List<StatModifier>();
            foreach(ModifiersSaveData savedMod in savedItem.modifiers)
            {
                StatModType statModType = new StatModType();

                switch(savedMod.modType)
                {
                    case 0:
                        statModType = StatModType.FLAT;
                        break;
                    case 1:
                        statModType = StatModType.PERCENT;
                        break;
                    case 2:
                        statModType = StatModType.FULLPERCENT;
                        break;
                }

                TargetStat targetStat = new TargetStat();

                switch(savedMod.targetStat)
                {
                    case 0:
                        targetStat = TargetStat.HEALTH;
                        break;

                    case 1:
                        targetStat = TargetStat.ATTACK;
                        break;

                    case 2:
                        targetStat = TargetStat.DEFENSE;
                        break;

                    case 3:
                        targetStat = TargetStat.ATTACKSPEED;
                        break;

                    case 4:
                        targetStat = TargetStat.CRITCHANCE;
                        break;
                }

                StatModifier mod = new StatModifier(savedMod.value, savedMod.growth, statModType, targetStat);
                modifiers.Add(mod);
            }

            GeneratedItem newItem = new GeneratedItem(savedItem.id, ItemDatabaseManager._instance.GetItemById(savedItem.id).itemName, ItemDatabaseManager._instance.GetItemById(savedItem.id).icon, modifiers, ItemDatabaseManager._instance.GetItemById(savedItem.id).maxLevel,ItemDatabaseManager._instance.GetItemById(savedItem.id).itemUpgradeCost);
            newItem.SetEquiped(savedItem.isEquiped);
            this.inventory.AddItemToInventory(newItem);
        }

        this.inventory.SetNonUpdated();
    }


}
