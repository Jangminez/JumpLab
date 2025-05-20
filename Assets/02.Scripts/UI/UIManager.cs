using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("PlayerUI")]
    [SerializeField] Image hpBar;
    [SerializeField] Image staminaBar;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        Player.onHealthChanged += UpdateHealthUI;
        Player.onStaminaChanged += UpdateStaminaUI;
    }

    void OnDestroy()
    {
        Player.onHealthChanged -= UpdateHealthUI;
        Player.onStaminaChanged -= UpdateStaminaUI;
    }

    void UpdateHealthUI(float maxValue, float curValue)
    {
        hpBar.fillAmount = curValue / maxValue;
    }

    void UpdateStaminaUI(float maxValue, float curValue)
    {
        staminaBar.fillAmount = curValue / maxValue;
    }
}
