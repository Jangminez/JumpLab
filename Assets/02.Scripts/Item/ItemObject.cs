using System;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Item1 = itemName,
    /// Item2 = description
    /// </summary>
    /// <returns></returns>
    public Tuple<string, string> GetItemInfo();
    public void InteractItem();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData dataSO;


    public Tuple<string, string> GetItemInfo()
    {
        Tuple<string, string> itemInfo = new(dataSO.itemName, dataSO.description);
        return itemInfo;
    }

    public void InteractItem()
    {
        PlayerInteractor.onGetItem?.Invoke(dataSO);
        Destroy(gameObject);
    }
}
