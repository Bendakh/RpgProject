using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInformationDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemStats;
    public void DisplayItem(GeneratedItem item)
    {
        itemName.text = item.itemName;
        itemStats.text = "";
        foreach(StatModifier mod in item.modifiers)
        {
            itemStats.text += mod.value + " " + mod.targetStat + " Growth : " + mod.growth;
        }
    }
}
