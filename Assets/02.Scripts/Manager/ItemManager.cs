using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemData selectItemSO;

    public void Init()
    {

    }

    public void SelectItem(ItemData data)
    {
        selectItemSO = data;
    }
}
