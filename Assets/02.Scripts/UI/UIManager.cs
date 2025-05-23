using System.Collections;
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
    [SerializeField] Image indicator;
    [SerializeField] float flashSpeed;
    private Coroutine indicatorCoroutine;

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
        player.Events.onTakeDamaged += Flash;
        player.Events.onInteractable += UpdateItemInfoUI;
    }

    void OnDestroy()
    {
        player.Events.onHealthChanged -= UpdateHealthUI;
        player.Events.onStaminaChanged -= UpdateStaminaUI;
        player.Events.onTakeDamaged -= Flash;
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

    public void Flash()
    {
        if (indicatorCoroutine != null)
            StopCoroutine(indicatorCoroutine);

        indicator.enabled = true;
        indicator.color = new Color(1f, 100f / 255f, 100f / 255f);
        indicatorCoroutine = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            indicator.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null;
        }

        indicator.enabled = false;
    }
}
