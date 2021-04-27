using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Loot
{
    public Sprite thisLoot;
    public int lootChance;
}



[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loot;
    public Sprite LootSprite()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for(int i = 0; i < loot.Length; i++)
        {
            cumProb += loot[i].lootChance;
            if(currentProb <= cumProb)
            {
                return loot[i].thisLoot;
            }
        }
        return null;
    }

}
