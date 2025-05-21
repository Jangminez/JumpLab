using UnityEngine;
using UnityEngine.InputSystem;

public class ToolBar : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] ItemSlot selectedSlot;
    [SerializeField] InputAction selectSlotAction;
    [SerializeField] Transform itemContainer;
    public ItemData selectedItemSO;

    void Awake()
    {
        itemSlots = GetComponentsInChildren<ItemSlot>();

        foreach (ItemSlot slot in itemSlots)
        {
            slot.Init(this);
        }

        selectedSlot = itemSlots[0];
        selectedSlot.SelectSlot();
    }
    void OnEnable()
    {
        PlayerInteractor.onGetItem += AddItemToSlot;
        selectSlotAction.started += OnSelectSlot;
        selectSlotAction.Enable();
    }

    void OnDestroy()
    {
        selectSlotAction.started -= OnSelectSlot;
        PlayerInteractor.onGetItem -= AddItemToSlot;
    }

    public void SelectSlot(ItemData data)
    {
        selectedItemSO = data;

        GameObject obj = Instantiate(data.dropPrefab, itemContainer);

        if (obj.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;
    }

    public void ClearHand()
    {
        if (itemContainer.childCount != 0)
            Destroy(itemContainer.GetChild(0).gameObject);
    }

    void AddItemToSlot(ItemData data)
    {
        ItemSlot slot = null;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].CanSetItem(data))
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

    # region InputAction
    public void OnSelectSlot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            int keyNum = int.Parse(context.control.name);
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
    #endregion

}
