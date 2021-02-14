using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Stuff/Item")]
public class Item : ScriptableObject
{
    public int id = -1;
    public string itemName = "New Item";
    public Sprite icon = null;
    public List<StatModifier> modifiers;

    public int maxLevel;
    public int itemUpgradeCost;
    /*public virtual void Use()
    {
        Debug.Log("Item " + itemName);

        EquipmentManager._instance.EquipItem(this);
    }*/

    public virtual GeneratedItem GenerateItem()
    {
        GeneratedItem ret = new GeneratedItem(this.id, this.itemName, this.icon, new List<StatModifier>(), this.maxLevel, this.itemUpgradeCost);

        foreach(StatModifier mod in modifiers)
        {
            ret.modifiers.Add(new StatModifier((int) Random.Range(mod.minValue, mod.maxValue + 1), (int) Random.Range(mod.minGrowth, mod.maxGrowth + 1), mod.modType, mod.targetStat));
        }

        //ret.

        return ret;
    }
}
