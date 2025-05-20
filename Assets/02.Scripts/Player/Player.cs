using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController playerController;
    PlayerInteractor playerInteractor;
    PlayerStats playerStats;

    void Awake()
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

    public void UseStamina(float value)
    {
        playerStats.ChangeStamina(-value);
    }

    public void Heal(float value)
    {
        playerStats.ChangeHealth(value);
    }
}
