using UnityEngine;

public enum ItemType
{
    Comsumable,
    Equipable,
}

public enum StatType
{
    Speed,
    Jump,
    DoubleJump
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

    [Header("Stat")]
    public StatType statType;
    public float value;
    public float duration;
}
