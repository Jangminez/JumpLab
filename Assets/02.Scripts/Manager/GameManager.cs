using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uIManager;
    public Player Player { get; private set; }
    public ToolBar ToolBar { get; private set; }

    protected override void Awake()
    {
        base.Awake();


        Player = FindObjectOfType<Player>();
        ToolBar = FindObjectOfType<ToolBar>();
        uIManager = GetComponentInChildren<UIManager>();

        if (Player)
            Player.Init(this);
        if (ToolBar)
            ToolBar.Init(this);
        if (uIManager)
            uIManager.Init(this);
    }

    public void UseItem()
    {
        ToolBar.UseItem(Player);
    }
}
