using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    ToolBar toolBar;
    PlayerController playerController;
    PlayerInteractor playerInteractor;
    PlayerStats playerStats;
    public PlayerEventHandler Events { get; private set; }
    public void Init(GameManager gameManager, ToolBar toolBar)
    {
        this.gameManager = gameManager;
        this.toolBar = toolBar;

        Events = new PlayerEventHandler();

        playerStats = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerController>();
        playerInteractor = GetComponent<PlayerInteractor>();

        if (playerStats)
            playerStats.Init(this);
        if (playerController)
            playerController.Init(this, playerStats);
        if (playerInteractor)
            playerInteractor.Init(this);

    }

    public void UseStamina(float value)
    {
        playerStats.ChangeStamina(-value);
    }

    public void TakeDamaged(float value)
    {
        playerStats.ChangeHealth(-value);
    }

    public void InteractItem()
    {
        playerInteractor.GetItem();
    }

    public void UseItem()
    {
        gameManager.ApplyItemEffect();
    }

    public void SetStats(StatType type, float value)
    {
        switch (type)
        {
            case StatType.Speed:
                playerStats.ChangeSpeed(value);
                break;

            case StatType.Jump:
                playerStats.ChangeJumpForce(value);
                break;

            case StatType.DoubleJump:
                playerStats.ChangeJumpCount(value);
                break;
        }
    }
}
