using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestingPanel : MonoBehaviour
{

    public static TestingPanel _instance;

    [SerializeField]
    Image itemIcon;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI modifiers;


    public void Awake()
    {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
    }
    public void DisplayGeneratedItem(GeneratedItem item)
    {
        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;
        modifiers.text = "Modifiers : ";
        foreach(StatModifier mod in item.modifiers)
        {
            modifiers.text += mod.targetStat + " " + mod.value;
        }
    }

    public void GenerateItem(int id)
    {
        GeneratedItem toDisplay = ItemDatabaseManager._instance.GenerateItem(id);
        DisplayGeneratedItem(toDisplay);
    }
}
