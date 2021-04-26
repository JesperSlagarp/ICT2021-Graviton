using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType{item, armor, weapon,}

public class StatModifier{

    public readonly int Value;
    public readonly object Source;

    public StatModifier(int value, StatModType type){
        Value = value;
    }




}