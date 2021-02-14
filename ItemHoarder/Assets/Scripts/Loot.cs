using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType
{
    ITEM,
    LOOTBOX_TIER1,
    LOOTBOX_TIER2,
    LOOTBOX_TIER3,
    BLACKSMITH_DUST
    
}

[Serializable]
public class Loot
{
    public string lootName;
    public LootType lootType;
    public float dropPercentage;
    public int quantity;

    //Item id, -1 if not item
    public int itemId;

    //public int minQuantity;
    //public int maxQuantity;
}



