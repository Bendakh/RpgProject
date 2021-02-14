using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int dmgAmount, Color color, FontStyles fontStyle)
    {
        damageText.SetText(dmgAmount.ToString());
        damageText.color = color;
        damageText.fontStyle = fontStyle;
    }
}
