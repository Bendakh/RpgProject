using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    FLAT,
    PERCENT,
    FULLPERCENT,
    ATTRBONUS
}

//Full percent is for stats like critical hit that are only in percentage

public enum TargetStat
{
    HEALTH,
    ATTACK,
    DEFENSE,
    ATTACKSPEED,
    CRITCHANCE
}

[System.Serializable]
public class StatModifier 
{
    public float value;
    public StatModType modType;
    public int order;
    public object source;
    public TargetStat targetStat;

    public float minValue;
    public float maxValue;

    //Growth in case the stat is with an item
    public float minGrowth;
    public float maxGrowth;
    public float growth;

    public StatModifier(float v, float g, StatModType type, int ord, object src, TargetStat trgt)
    {
        value = v;
        growth = g;
        modType = type;
        order = (int) type;
        source = src;
        targetStat = trgt;
    }

    public StatModifier(float v, float g, StatModType type, TargetStat trgt) : this(v, g, type, (int)type, null, trgt) { }
    
    public StatModifier(float v, float g, StatModType type, int ord, TargetStat trgt) : this (v, g, type, ord, null, trgt) { }

    public StatModifier(float v, float g, StatModType type, object src, TargetStat trgt) : this(v, g, type, (int)type, src, trgt) { }
}
