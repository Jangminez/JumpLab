using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("PlayerUI")]
    [SerializeField] Image hpBar;
    [SerializeField] Image staminaBar;

    [Header("Item UI")]
    [SerializeField] GameObject itemInfoUI;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        PlayerStats.onHealthChanged += UpdateHealthUI;
        PlayerStats.onStaminaChanged += UpdateStaminaUI;
        PlayerInteractor.onInteractable += UpdateItemInfoUI;
    }

    void OnDestroy()
    {
        PlayerStats.onHealthChanged -= UpdateHealthUI;
        PlayerStats.onStaminaChanged -= UpdateStaminaUI;
        PlayerInteractor.onInteractable -= UpdateItemInfoUI;
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
