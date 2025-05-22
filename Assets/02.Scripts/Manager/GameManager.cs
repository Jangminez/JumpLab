using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private ItemManager itemManager;
    private Player player;
    private ToolBar toolBar;
    private ItemData itemDataSO;
    private List<ItemData> equipItems = new List<ItemData>();

    protected override void Awake()
    {
        base.Awake();

        player = FindObjectOfType<Player>();
        toolBar = FindObjectOfType<ToolBar>();
        uIManager = GetComponentInChildren<UIManager>();

        if (player)
            player.Init(this, toolBar);
        if (toolBar)
            toolBar.Init(this, player);
        if (uIManager)
            uIManager.Init(this, player);
    }

    public void SetItemData(ItemData data)
    {
        itemDataSO = data;
    }

    public void ApplyItemEffect()
    {
        if (itemDataSO == null) return;

        switch (itemDataSO.itemType)
        {
            case ItemType.Comsumable:
                StartCoroutine(ApplyPotionEffect(itemDataSO));
                break;

            case ItemType.Equipable:
                ApplyEquipItem(itemDataSO);
                break;
        }

        toolBar.UseSlotItem();
    }

    IEnumerator ApplyPotionEffect(ItemData data)
    {
        player.SetStats(data.statType, data.value);

        yield return new WaitForSeconds(data.duration);

        player.SetStats(data.statType, -data.value);
    }

    public void ApplyEquipItem(ItemData data)
    {
        if (!equipItems.Contains(data))
        {
            equipItems.Add(data);
            player.SetStats(data.statType, data.value);
        }
        else
        {
            equipItems.Remove(data);
            player.SetStats(data.statType, -data.value);
        }
    }
}
