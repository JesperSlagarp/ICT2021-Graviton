using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stats
{

    private List<StatModifier> modifiers = new List<StatModifier>();

    public int GetValue(int baseValue)
    {
        return CalculateEndVal(baseValue);
    }

    // player.damage.addmodifier()
    //object måste ha StatModifier script på sig.
    public void AddModifier(StatModifier mod)
    {
        if (mod.Value != 0)
        {
            modifiers.Add(mod);
        }
    }
    public void RemoveModifier(StatModifier mod)
    {
        if (mod.Value != 0)
        {
            modifiers.Remove(mod);
        }
    }
    private int CalculateEndVal(int BaseValue)
    {
        int EndValue = BaseValue;
        for (int i = 0; i < modifiers.Count; i++)
        {
            EndValue += modifiers[i].Value;
        }
        return EndValue;

    }

}
