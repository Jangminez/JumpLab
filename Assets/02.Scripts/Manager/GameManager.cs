using System.Data.Common;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uIManager;
    public Player Player { get; private set; }
    public ItemData SelectItemSO { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        uIManager = GetComponentInChildren<UIManager>();
        Player = FindObjectOfType<Player>();

        if (uIManager)
            uIManager.Init(this);

        if (Player)
            Player.Init();
    }

    public void SelectItem(ItemData data)
    {
        SelectItemSO = data;
    }

    public void UseItem()
    {
        if (SelectItemSO != null)
            SelectItemSO.UseItem(Player);
    }
}
