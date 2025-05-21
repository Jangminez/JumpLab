using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    Player player;

    [Header("PlayerStats")]
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float maxStamina;
    [SerializeField] float stamina;
    [SerializeField] float regenHealth;
    [SerializeField] float regenStamina;
    [SerializeField] float regenRate;

    bool isDie = false;

    public void Init(Player player)
    {
        this.player = player;

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
        player.Events.RaisedHealthChanged(maxHealth, health);

        if (health <= 0f)
        {
            Die();
        }
    }

    public void ChangeStamina(float value)
    {
        stamina = Mathf.Clamp(stamina + value, 0f, maxStamina);
        player.Events.RaisedStaminaChanged(maxStamina, stamina);
    }

    void Die()
    {
        isDie = true;
    }
}
