using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController playerController;
    PlayerInteractor playerInteractor;
    PlayerStats playerStats;

    public void Init()
    {
        playerController = GetComponent<PlayerController>();
        playerInteractor = GetComponent<PlayerInteractor>();
        playerStats = GetComponent<PlayerStats>();

        if (playerController)
            playerController.Init(this);
        if (playerInteractor)
            playerInteractor.Init(this);
        if (playerStats)
            playerStats.Init();
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
        playerInteractor.InteractItem();
    }
}
