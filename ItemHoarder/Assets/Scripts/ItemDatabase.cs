using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Stuff/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> ItemList;
}
