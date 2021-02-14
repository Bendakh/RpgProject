using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneratedItem 
{
    
    public int id;
    public string itemName;
    [NonSerialized]
    public Sprite icon;
    public List<StatModifier> modifiers;
    public int currentLevel;
    public int maxLevel;
    public int itemUpgradeCost;

    private bool isEquiped = false;

    public GeneratedItem(int id, string itemName, Sprite icon, List<StatModifier> mods, int maxLevel, int itemUpgradeCost)
    {
        this.id = id;
        this.itemName = itemName;
        this.icon = icon;
        this.modifiers = mods;
        this.maxLevel = maxLevel;
        this.itemUpgradeCost = itemUpgradeCost;
    }

    public bool IsEquiped { get => isEquiped; }

    public void SetEquiped(bool equip)
    {
        this.isEquiped = equip;
    }

    public void ToggleEquiped()
    {
        isEquiped = !isEquiped;
    }


    //Final fix can't upgrade an item if it's equiped
    public void UpgradeItem()
    {
        if(currentLevel < maxLevel && GameManager._instance.player.Currency > itemUpgradeCost)
        {
            currentLevel++;
            foreach(StatModifier mod in modifiers)
            {
                mod.value += mod.growth;
                switch(mod.targetStat)
                {
                    case TargetStat.HEALTH:
                        GameManager._instance.player.maxHealth.SetDirty();
                        break;
                    case TargetStat.DEFENSE:
                        GameManager._instance.player.armor.SetDirty();
                        break;
                    case TargetStat.ATTACK:
                        GameManager._instance.player.damage.SetDirty();
                        break;
                    case TargetStat.ATTACKSPEED:
                        GameManager._instance.player.attackSpeed.SetDirty();
                        break;
                    case TargetStat.CRITCHANCE:
                        GameManager._instance.player.critChance.SetDirty();
                        break;
                }
            }
            GameManager._instance.player.GetCurrency(-itemUpgradeCost);
        }
    }
}
