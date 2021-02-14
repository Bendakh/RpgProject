using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI attrPtsDisplay;

    [SerializeField]
    TextMeshProUGUI healthDisplay;
    [SerializeField]
    TextMeshProUGUI damageDisplay;
    [SerializeField]
    TextMeshProUGUI armorDisplay;
    [SerializeField]
    TextMeshProUGUI attackSpeedDisplay;
    [SerializeField]
    TextMeshProUGUI criticalChanceDisplay;

    [SerializeField]
    TextMeshProUGUI healthBonusDisplay;
    [SerializeField]
    TextMeshProUGUI damageBonusDisplay;
    [SerializeField]
    TextMeshProUGUI armorBonusDisplay;
    [SerializeField]
    TextMeshProUGUI attackSpeedBonusDisplay;
    [SerializeField]
    TextMeshProUGUI criticalChanceBonusDisplay;

    [SerializeField]
    Button confirmAttrPointAllocationButton;

    [SerializeField]
    Button addHealthPoints;

    private int tempHp;
    private int tempDamage;
    private int tempArmor;
    private int tempAS;
    private int tempCritChance;

    public void UpdateDisplay()
    {
        attrPtsDisplay.text = "Attribute Points : " + GameManager._instance.player.AttributePoints;
        healthDisplay.text = "Health : " + GameManager._instance.player.currentHealth + "/" + GameManager._instance.player.maxHealth.Value;
        damageDisplay.text = "Damage : " + GameManager._instance.player.damage.Value;
        armorDisplay.text = "Armor : " + GameManager._instance.player.armor.Value;
        attackSpeedDisplay.text = "Attack speed : " + GameManager._instance.player.attackSpeed.Value;
        criticalChanceDisplay.text = "Crit Chance : " + GameManager._instance.player.critChance.Value;
    }

    //1 for health, 2 for damage, 3 for armor and 4 for attackspeed
    public void AddPointsButton(int stat)
    {
        
        if (GameManager._instance.player.AttributePoints > 0)
        {
            switch (stat)
            {
                case 1:
                    tempHp++;
                    healthBonusDisplay.text = "+" + tempHp;
                    break;
                case 2:
                    tempDamage++;
                    damageBonusDisplay.text = "+" + tempDamage;
                    break;
                case 3:
                    tempArmor++;
                    armorBonusDisplay.text = "+" + tempArmor;
                    break;
                case 4:
                    tempAS++;
                    attackSpeedBonusDisplay.text = "+" + tempAS;
                    break;
                case 5:
                    tempCritChance++;
                    criticalChanceBonusDisplay.text = "+" + tempCritChance;
                    break;
            }

            GameManager._instance.player.AttributePoints--;
        }
        UpdateDisplay();
    }

    public void ConfirmAttrPointsButton()
    {
        if(tempHp > 0 || tempDamage > 0 || tempArmor > 0 || tempAS > 0 || tempCritChance > 0)
        {
            
            GameManager._instance.player.ApplyBonuses(tempHp, tempDamage, tempArmor, tempAS, tempCritChance);
            tempHp = 0;
            tempDamage = 0;
            tempArmor = 0;
            tempAS = 0;
            tempCritChance = 0;
            UpdateDisplay();
            ResetBonusDisplays();
        }
    }

    private void ResetBonusDisplays()
    {
        healthBonusDisplay.text = "";   
        damageBonusDisplay.text = "";
        armorBonusDisplay.text = "";
        attackSpeedBonusDisplay.text = "";
        criticalChanceBonusDisplay.text = "";
    }
}
