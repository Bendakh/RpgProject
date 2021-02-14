using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager _instance;

    [SerializeField]
    Player playerStats;
    [SerializeField]
    Enemy enemyStats;
    [SerializeField]
    Image attackLoaderBar;
    [SerializeField]
    TextMeshProUGUI attackLoaderText;
    [SerializeField]
    Button characterPanelButton;
    [SerializeField]
    GameObject characterPanel;
    [SerializeField]
    Image playerHpBar;
    [SerializeField]
    Image playerManaBar;
    [SerializeField]
    TextMeshProUGUI playerHpText;
    [SerializeField]
    TextMeshProUGUI playerManaText;
    [SerializeField]
    GameObject itemPanel;
    [SerializeField]
    GameObject enemyPanel;

    [SerializeField]
    Image enemyHpBar;

    [SerializeField]
    GameObject exclamationMark;

    [SerializeField]
    Image playerXpBar;
    [SerializeField]
    TextMeshProUGUI playerXpText;

    [SerializeField]
    TextMeshProUGUI currencyDisplay;

    [SerializeField]
    Button startCombatButton;

    [SerializeField]
    TextMeshProUGUI enemyName;

    [SerializeField]
    Button saveGameButton;

    private bool isCharacterPanelDisplayed;

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

    // Start is called before the first frame update
    void Start()
    {
        isCharacterPanelDisplayed = false;
        saveGameButton.onClick.AddListener(() => SaveLoadManager.SavePlayerData(GameManager._instance.player));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.IsFighting && startCombatButton.interactable)
        {
            startCombatButton.interactable = false;
            enemyPanel.SetActive(true);
        }
        else if (!GameManager._instance.IsFighting && !startCombatButton.interactable)
        {
            startCombatButton.interactable = true;
            enemyPanel.SetActive(false);
        }
        UpdateAttackBar();
        UpdatePlayerBars();
        UpdateUI();

        if (enemyStats.currentEnemy != null)
        {
            enemyHpBar.fillAmount = (float)enemyStats.currentHealth / (float)enemyStats.currentEnemy.maxHealth;
            enemyName.text = enemyStats.currentEnemy.enemyName;
        }
    }

    private void UpdateAttackBar()
    {
        attackLoaderBar.fillAmount = (playerStats.attackSpeed.Value - playerStats.GetAttackCounter()) / playerStats.attackSpeed.Value;
        attackLoaderText.text = playerStats.GetAttackCounter().ToString("F2");
    }

    private void UpdatePlayerBars()
    {
        playerHpBar.fillAmount = playerStats.currentHealth / playerStats.maxHealth.Value;
        playerManaBar.fillAmount = playerStats.currentMana / playerStats.maxMana.Value;
        playerXpBar.fillAmount = playerStats.CurrentXp / playerStats.ToReachXp;

        playerHpText.text = playerStats.currentHealth + "/" + playerStats.maxHealth.Value;
        playerManaText.text = playerStats.currentMana + "/" + playerStats.maxMana.Value;
        playerXpText.text = playerStats.CurrentXp + "/" + playerStats.ToReachXp;

        currencyDisplay.text = "Currency : " + playerStats.Currency;
    }

    public void DisplayHideCharacterPanel()
    {
        if (!isCharacterPanelDisplayed)
        {
            characterPanel.SetActive(true);
            characterPanel.SendMessage("UpdateDisplay");
            //characterPanelButton.interactable = false;
            isCharacterPanelDisplayed = !isCharacterPanelDisplayed;
        }
        else
        {
            characterPanel.SetActive(false);
            isCharacterPanelDisplayed = !isCharacterPanelDisplayed;
        }
    }

    public void DisplayItemPanel(GeneratedItem item)
    {
        itemPanel.SetActive(true);
        itemPanel.SendMessage("Initialize", item);
    }

    public void HideItemPanel()
    {
        itemPanel.SendMessage("ResetPanel");
        itemPanel.SetActive(false);
    }

    public void StartCombatButton()
    {
        GameManager._instance.StartCombat();
    }

    private void UpdateUI()
    {
        if(GameManager._instance.player.AttributePoints > 0)
        {
            exclamationMark.SetActive(true);
        }
        else
        {
            exclamationMark.SetActive(false);
        }
    }
}
