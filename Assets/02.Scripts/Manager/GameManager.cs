using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private ItemManager itemManager;
    private Player player;
    private ToolBar toolBar;

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
}
