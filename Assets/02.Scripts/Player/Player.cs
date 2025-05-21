using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;
    PlayerInteractor playerInteractor;
    PlayerStats playerStats;
    public PlayerEventHandler Events { get; private set; }
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        Events = new PlayerEventHandler();

        playerController = GetComponent<PlayerController>();
        playerInteractor = GetComponent<PlayerInteractor>();
        playerStats = GetComponent<PlayerStats>();

        if (playerController)
            playerController.Init(this);
        if (playerInteractor)
            playerInteractor.Init(this);
        if (playerStats)
            playerStats.Init(this);
    }

    public void HealStamina(float value)
    {
        playerStats.ChangeStamina(value);
    }

    public void UseStamina(float value)
    {
        playerStats.ChangeStamina(-value);
    }

    public void HealHealth(float value)
    {
        playerStats.ChangeHealth(value);
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
        gameManager.UseItem();
    }
}
