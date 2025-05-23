using System;

public class PlayerEventHandler
{
    public event Action<float, float> onHealthChanged;
    public event Action<float, float> onStaminaChanged;
    public event Action onTakeDamaged;
    public event Action<string, string> onInteractable;
    public event Action<ItemData> onGetItem;

    public void RaisedHealthChanged(float maxHealth, float curHealth) => onHealthChanged?.Invoke(maxHealth, curHealth);
    public void RaisedStaminaChanged(float maxStamina, float curStamina) => onStaminaChanged?.Invoke(maxStamina, curStamina);
    public void RaisedTakeDamaged() => onTakeDamaged?.Invoke();
    public void RaisedInteracted(string itemName, string description) => onInteractable?.Invoke(itemName, description);
    public void RasiedGetItem(ItemData itemSO) => onGetItem?.Invoke(itemSO);
}
