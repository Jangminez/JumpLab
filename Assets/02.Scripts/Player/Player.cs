using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController playerController;

    [Header("PlayerStats")]
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float maxStamina;
    [SerializeField] float stamina;
    [SerializeField] float regenHealth;
    [SerializeField] float regenStamina;
    [SerializeField] float regenRate;

    bool isDie = false;
    public static Action<float, float> onHealthChanged;
    public static Action<float, float> onStaminaChanged;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        if (playerController)
            playerController.Init(this);

    }
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;

        StartCoroutine(RegenStats());
    }

    IEnumerator RegenStats()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(regenRate);

            ChangeHealth(regenHealth);
            ChangeStamina(regenStamina);
        }
    }

    public void ChangeHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0f, maxHealth);
        onHealthChanged?.Invoke(maxHealth, health);

        if (health <= 0f)
        {
            Die();
        }
    }

    public void ChangeStamina(float value)
    {
        stamina = Mathf.Clamp(stamina + value, 0f, maxStamina);
        onStaminaChanged?.Invoke(maxStamina, stamina);
    }

    void Die()
    {
        // When Player Die
        isDie = true;
    }
}
