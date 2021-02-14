using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public int attack;
    public int defense;
    public float attackModifier;

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.G))
        {
            EquipmentManager._instance.EquipItem(ItemDatabaseManager._instance.GetItemById(0));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            EquipmentManager._instance.UnequipItem(ItemDatabaseManager._instance.GetItemById(0));
        }*/
    }

    private int CalculateDamage()
    {
        return Mathf.FloorToInt(((float)attack * (1 + Random.Range(-attackModifier, attackModifier))) * (100 / (100 + (float)defense)));
       
    }
}
