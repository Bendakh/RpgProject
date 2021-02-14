using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    GeneratedItem itemDisplayed;

    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemLevel;
    [SerializeField]
    TextMeshProUGUI itemStats;

    [SerializeField]
    Button upgradeButton;

    [SerializeField]
    TextMeshProUGUI upgradeButtonText;

    [SerializeField]
    TextMeshProUGUI equipButtonText;

    public void Initialize(GeneratedItem item)
    {
        itemDisplayed = item;
        itemName.text = item.itemName;
        itemLevel.text = item.currentLevel + "/" + item.maxLevel;
        itemStats.text = "";
        foreach (StatModifier mod in item.modifiers)
        {
            itemStats.text += mod.value + " " + mod.targetStat + "\n";
        }

        if(itemDisplayed.IsEquiped)
        {
            equipButtonText.text = "Unequip";
        }
        else
        {
            equipButtonText.text = "Equip";
        }
    }

    public void ResetPanel()
    {
        itemName.text = "";
        itemLevel.text = "";
        itemStats.text = "";
    }

    public void EquipItemButton()
    {
        EquipmentManager._instance.EquipUnequipItem(itemDisplayed);
        if (itemDisplayed.IsEquiped)
            equipButtonText.text = "Unequip";
        else
            equipButtonText.text = "Equip";
    }

    public void UpgradeItemButton()
    {
        itemDisplayed.UpgradeItem();
        UpdateDisplay();

        if(itemDisplayed.currentLevel >= itemDisplayed.maxLevel)
        {
            upgradeButton.interactable = false;
            upgradeButtonText.text = "MAX";
        }
    }

    private void UpdateDisplay()
    {
        itemName.text = itemDisplayed.itemName;
        itemLevel.text = itemDisplayed.currentLevel + "/" + itemDisplayed.maxLevel;
        itemStats.text = "";
        foreach (StatModifier mod in itemDisplayed.modifiers)
        {
            itemStats.text += mod.value + " " + mod.targetStat + "\n";
        }
    }
}
