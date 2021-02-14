using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public class Stat
{ 

    [SerializeField]
    private float baseValue;

    public float Value { 
        get { 
            if(isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }            
            return _value; 
        } 
    }

    private bool isDirty = true;
    private float _value;
    private float lastBaseValue = float.MinValue;

    //SET THIS TO PRIVATE WHEN FINISHED TESTING 
    [SerializeField]
    private List<StatModifier> modifiers = new List<StatModifier>();
    //public ReadOnlyCollection<StatModifier> Modifiers = modifiers.AsReadOnly();

    public Stat(float baseV)
    {
        baseValue = baseV;
        modifiers = new List<StatModifier>();
        
        //Modifiers = modifiers.AsReadOnly();
    }

    public float GetValue()
    {
        return baseValue;
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        modifiers.Add(mod);
        modifiers.Sort(CompareModifierOrder);     
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }

    public bool RemoveModifier(StatModifier mod)
    {
        if(modifiers.Remove(mod))
        {
            isDirty = true;
            
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object src)
    {
        bool didRemove = false;

        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            if(modifiers[i].source == src)
            {
                isDirty = true;
                didRemove = true;
                modifiers.RemoveAt(i);
            }
        }
        
        return didRemove;
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;
        float sumFullPercentAdd = 0;
        for(int i = 0; i < modifiers.Count; i++)
        {
            StatModifier mod = modifiers[i];

            if (mod.modType == StatModType.FLAT)
            {
                finalValue += mod.value;
            }
            else if (mod.modType == StatModType.PERCENT)
            {
                sumPercentAdd += mod.value;
                if (i + 1 >= modifiers.Count || modifiers[i + 1].modType != StatModType.PERCENT)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.modType == StatModType.FULLPERCENT)
            {
                sumFullPercentAdd += mod.value;
                if (i + 1 >= modifiers.Count || modifiers[i + 1].modType != StatModType.FULLPERCENT)
                {
                    finalValue += sumFullPercentAdd;
                    sumFullPercentAdd = 0;
                }
            }
                       
        }
        //return Mathf.FloorToInt(finalValue);
        return finalValue;
    }

    public void SetDirty()
    {
        this.isDirty = true;
    }
}
