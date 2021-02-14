using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    /*How the loot system works:
     * First we use use probability to determine how much loot we gonna get 100% for 0 loot for example, 50% for 2 loots etc etc
     * Then for each loot instance we use loop through the loot table and see the probability for each loot and see each time what loot the player gets
     */

    public static LootSystem _instance;

    private void Awake()
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

    
    public List<Loot> GetLoot(LootTable lootTable)
    {
        List<Loot> lootToGet = new List<Loot>();
        foreach(Loot loot in lootTable.lootList)
        {
            if(Random.Range(0f,100f) <= loot.dropPercentage)
            {
                lootToGet.Add(loot);
            }
        }

        return lootToGet;
    }
}
