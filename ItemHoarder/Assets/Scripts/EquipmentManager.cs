using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    public static EquipmentManager _instance;

    private Player playerStats;
    private void Awake()
    {
        _instance = this;
        
        if(playerStats == null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    #endregion

    [SerializeField]
    Sound equipSoundEffect;

    public List<GeneratedItem> equippedItems;

    private void Start()
    {
        equippedItems = new List<GeneratedItem>();
    }

    public void EquipUnequipItem(GeneratedItem item)
    {
        if (!item.IsEquiped)
        {
            equippedItems.Add(item);
            playerStats.AddItemModifiers(item);
            item.ToggleEquiped();
            AudioManager._instance.PlaySound(equipSoundEffect);
        }
        else
        {
            playerStats.RemoveItemModifiers(item);
            equippedItems.Remove(item);
            item.ToggleEquiped();
        }
    }

    /*public void UnequipItem(GeneratedItem item)
    {
        if (item.IsEquiped)
        {
            playerStats.RemoveItemModifiers(item);
            equippedItems.Remove(item);
            item.ToggleEquiped();
        }
    }*/
}
