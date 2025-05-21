using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uIManager;
    public Player Player { get; private set; }
    public ToolBar ToolBar { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        uIManager = GetComponentInChildren<UIManager>();
        Player = FindObjectOfType<Player>();
        ToolBar = FindObjectOfType<ToolBar>();

        if (uIManager)
            uIManager.Init(this);

        if (Player)
            Player.Init(this);

        if (ToolBar)
            ToolBar.Init();
    }

    public void UseItem()
    {
        ToolBar.UseItem(Player);
    }
}
