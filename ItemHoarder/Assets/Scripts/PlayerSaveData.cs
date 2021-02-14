using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerSaveData 
{

    public int level;
    public int currency;
    public int currentExp;
    public int toReachXp;
    public int attributePoints;

    public int currentHealth;
    public int currentMana;

    public int hpBonus;
    public int damageBonus;
    public int armorBonus;
    public int attackSpeedBonus;
    public int critChanceBonus;

    public ItemSaveData[] inventoryItems;

    public PlayerSaveData(Player player)
    {
        level = player.Level;
        currency = player.Currency;
        currentExp = player.CurrentXp;
        toReachXp = player.ToReachXp;
        attributePoints = player.AttributePoints;

        currentHealth = player.currentHealth;
        currentMana = player.currentMana;

        hpBonus = player.HpBonus;
        damageBonus = player.DamageBonus;
        armorBonus = player.ArmorBonus;
        attackSpeedBonus = player.AttackSpeedBonus;
        critChanceBonus = player.CritChanceBonus;

       
        inventoryItems = new ItemSaveData[player.PlayerInventory.inventory.Count];

        
        for (int i = 0; i < player.PlayerInventory.inventory.Count; i++)
        {
            inventoryItems[i] = new ItemSaveData(player.PlayerInventory.inventory[i]); 
        }
    }
}

[Serializable]
public class ItemSaveData
{
    public int id;
    public List<ModifiersSaveData> modifiers;
    public int currentLevel;

    public bool isEquiped;
    // 1 if is equiped, 0 else
    //public int isEquiped;
    
    public ItemSaveData(GeneratedItem item)
    {
        this.modifiers = new List<ModifiersSaveData>();
        this.id = item.id;

        foreach (StatModifier mod in item.modifiers)
        {
            this.modifiers.Add(new ModifiersSaveData(mod));
        }

        this.currentLevel = item.currentLevel;
        this.isEquiped = item.IsEquiped;
        //this.isEquiped = item.IsEquiped ? 1 : 0;
    }
}

[Serializable]
public class ModifiersSaveData
{
    public float value;
    public int modType;
    public int order;
    public object source;
    public int targetStat;
    public float growth;

    public ModifiersSaveData(StatModifier statModifier)
    {
        value = statModifier.value;
        modType = (int)statModifier.modType;
        order = statModifier.order;
        source = statModifier.source;
        targetStat = (int)statModifier.targetStat;
        growth = statModifier.growth;
    }
}
