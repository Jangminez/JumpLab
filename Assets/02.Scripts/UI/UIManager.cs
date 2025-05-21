using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;


    [Header("PlayerUI")]
    [SerializeField] Image hpBar;
    [SerializeField] Image staminaBar;

    [Header("Item UI")]
    [SerializeField] GameObject itemInfoUI;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    public void Init(GameManager gameManager, Player player)
    {
        this.gameManager = gameManager;
        this.player = player;

        player.Events.onHealthChanged += UpdateHealthUI;
        player.Events.onStaminaChanged += UpdateStaminaUI;
        player.Events.onInteractable += UpdateItemInfoUI;
    }

    void OnDestroy()
    {
        player.Events.onHealthChanged -= UpdateHealthUI;
        player.Events.onStaminaChanged -= UpdateStaminaUI;
        player.Events.onInteractable -= UpdateItemInfoUI;
    }

    void UpdateHealthUI(float maxValue, float curValue)
    {
        hpBar.fillAmount = curValue / maxValue;
    }

    void UpdateStaminaUI(float maxValue, float curValue)
    {
        staminaBar.fillAmount = curValue / maxValue;
    }

    void UpdateItemInfoUI(string itemName, string description)
    {
        if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(description))
        {
            itemInfoUI.SetActive(false);
            return;
        }

        itemInfoUI.SetActive(true);

        itemNameText.text = itemName;
        itemDescriptionText.text = description;
    }
}
