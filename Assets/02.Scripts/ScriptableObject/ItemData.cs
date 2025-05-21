using UnityEngine;

public enum ItemType
{
    Comsumable,
    Equipable,
}

public enum PotionType
{
    Health,
    Stamina
}

[CreateAssetMenu(fileName = "Item", menuName = "Item/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public Sprite itemIcon;
    [TextArea(3, 10)]
    public string description;
    public ItemType itemType;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxAmount;

    [Header("Potion")]
    public PotionType potionType;
    public float value;

    public void UseItem(Player player)
    {
        switch (itemType)
        {
            case ItemType.Comsumable:
                if (potionType == PotionType.Health)
                    player.HealHealth(value);
                else
                    player.HealStamina(value);
                break;

            case ItemType.Equipable:
                Debug.Log("Equip Item");
                break;
        }
    }
}
