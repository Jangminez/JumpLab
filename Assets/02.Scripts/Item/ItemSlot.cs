using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private ToolBar toolBar;
    ItemData dataSO;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] Image itemIcon;
    [SerializeField] Outline outline;
    private Color myColor;
    private int quantity;

    public void Init(ToolBar toolBar)
    {
        this.toolBar = toolBar;

        quantityText = GetComponentInChildren<TextMeshProUGUI>();
        itemIcon =  transform.GetChild(0).GetComponent<Image>();
        outline = GetComponent<Outline>();

        if (quantityText)
            quantityText.gameObject.SetActive(false);

        if (itemIcon)
            itemIcon.gameObject.SetActive(false);

        if (outline)
                myColor = outline.effectColor;
    }

    public void SetSlot(ItemData data)
    {
        if (dataSO != data)
        {
            dataSO = data;
            quantity = 1;
        }
        else
        {
            quantity += 1;
        }

        quantityText.gameObject.SetActive(dataSO.canStack);
        quantityText.text = quantity.ToString();

        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = data.itemIcon;
    }

    public void ClearSlot()
    {
        dataSO = null;
        quantity = 0;
        quantityText.gameObject.SetActive(false);
        itemIcon.gameObject.SetActive(false);
    }

    public void SelectSlot()
    {
        outline.effectColor = Color.white;

        if (dataSO != null)
        {
            toolBar.SelectSlot(dataSO);
        }
    }

    public void DeSelectSlot()
    {
        outline.effectColor = myColor;
    }

    public void UseItem()
    {
        quantity -= 1;

        if (quantity == 0)
        {
            ClearSlot();
            toolBar.ClearHand();
        }
    }

    public bool CanAddItem(ItemData data)
    {
        if (dataSO != null)
        {
            if (data == dataSO && quantity < dataSO.maxAmount)
                return true;
            else
                return false;
        }

        return true;
    }
}
