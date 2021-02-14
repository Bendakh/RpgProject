using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GeneratedItem> inventory = new List<GeneratedItem>();

    //when we add an item we set this variable to false and when we update we set it to true, this or we destroy all children in the inventory when we close it 
    private bool isUpdated = true;

    [SerializeField]
    GameObject slotsContainer;

    [SerializeField]
    GameObject slotPrefab;

    private void Start()
    {
        //inventory.Add(ItemDatabaseManager._instance.GenerateItem(0));
        //inventory.Add(ItemDatabaseManager._instance.GenerateItem(1));
        isUpdated = false;
        //UpdateInventory(); 
    }


    public void AddItemToInventory(GeneratedItem item)
    {
        inventory.Add(item);
    }

    public void DisplayInventory()
    {
        slotsContainer.SetActive(true);
        if(!isUpdated)
            UpdateInventory();
    }
    public void HideInventory()
    {
        
        slotsContainer.SetActive(false);
    }


    private void UpdateInventory()
    {
        foreach(GeneratedItem item in inventory)
        {
            ItemSlot itemSlot = Instantiate(slotPrefab, slotsContainer.transform).GetComponent<ItemSlot>();
            itemSlot.DisplaySlot(item);
            
        }

        isUpdated = true;
    }

    public void SetNonUpdated()
    {
        this.isUpdated = false;
    }
}
