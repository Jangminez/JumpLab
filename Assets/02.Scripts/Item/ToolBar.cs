using UnityEngine;
using UnityEngine.InputSystem;

public class ToolBar : MonoBehaviour
{
    GameManager gameManager;
    Player player;

    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] ItemSlot selectedSlot;
    [SerializeField] Transform itemContainer;
    private ItemData selectedItemSO;

    public void Init(GameManager gameManager, Player player)
    {
        this.gameManager = gameManager;
        this.player = player;

        itemSlots = GetComponentsInChildren<ItemSlot>();

        foreach (ItemSlot slot in itemSlots)
        {
            slot.Init(this);
        }

        selectedSlot = itemSlots[0];
        selectedSlot.SelectSlot();

        // Subscribe Events
        player.Events.onGetItem += AddItemToSlot;
    }

    void OnDestroy()
    {
        player.Events.onGetItem -= AddItemToSlot;
    }

    public void SelectSlot(ItemData data)
    {
        if (selectedItemSO != data)
        {
            selectedItemSO = data;

            GameObject obj = Instantiate(data.dropPrefab, itemContainer);

            if (obj.TryGetComponent(out Rigidbody rb))
                rb.isKinematic = true;
            if (obj.TryGetComponent(out Collider col))
                col.enabled = false;

            gameManager.SetItemData(selectedItemSO);
        }
    }

    public void UseSlotItem()
    {
        selectedSlot.UseItem();
    }

    public void ClearHand()
    {
        if (itemContainer.childCount > 0)
        {
            Destroy(itemContainer.GetChild(0).gameObject);
        }

        selectedItemSO = null;
        gameManager.SetItemData(selectedItemSO);
    }

    void AddItemToSlot(ItemData data)
    {
        ItemSlot slot = null;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].CanAddItem(data))
            {
                slot = itemSlots[i];
                slot.SetSlot(data);

                if (slot == selectedSlot)
                    slot.SelectSlot();

                break;
            }
        }

        if (slot == null)
        {
            DropItem(data);
        }
    }

    void DropItem(ItemData data)
    {
        Instantiate(data.dropPrefab, Camera.main.transform.position + Vector3.forward * 2, Quaternion.identity);
    }

    #region InputAction
    public void OnSelectSlot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            string keyString = context.control.name;

            if (int.TryParse(keyString, out int keyNum))
            {
                int slotIdx = keyNum - 1 != -1 ? keyNum - 1 : itemSlots.Length - 1;

                ItemSlot slot = itemSlots[slotIdx];

                if (selectedSlot != null && selectedSlot != slot)
                {
                    selectedSlot.DeSelectSlot();
                    ClearHand();
                }

                selectedSlot = slot;
                selectedSlot.SelectSlot();
            }
        }
    }
    #endregion
}
