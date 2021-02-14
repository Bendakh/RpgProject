using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseManager : MonoBehaviour
{
    public static ItemDatabaseManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private ItemDatabase itemDatabase;

    public Item GetItemById(int id)
    {
        Item ret = itemDatabase.ItemList[id];
        if (ret)
        {
            return ret;
        }
        else
        {
            Debug.LogError("There is no item with the id " + id);
            return null;
        }
    }

    public GeneratedItem GenerateItem(int id)
    {
        GeneratedItem ret = GetItemById(id).GenerateItem();
        return ret;
    }
    
}
