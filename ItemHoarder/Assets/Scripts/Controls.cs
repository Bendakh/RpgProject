using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    [SerializeField]
    GameObject inventory;
    bool isInventoryDisplayed;

    private void Start()
    {
        isInventoryDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }
    }


    private void DisplayInventory()
    {
        if(!isInventoryDisplayed)
        {
            inventory.SendMessage("DisplayInventory");
            isInventoryDisplayed = true;
        }
        else
        {
            inventory.SendMessage("HideInventory");
            isInventoryDisplayed = false;
        }
        
    }
}
