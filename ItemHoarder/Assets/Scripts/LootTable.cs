using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LootTable", menuName = "LootTable")]
public class LootTable : ScriptableObject
{
    public List<Loot> lootList = new List<Loot>();
}
