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

    public float Stamina { get => stamina; }

    [Header("MovementStats")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] int maxJumpCount = 1;
    public float MoveSpeed { get => moveSpeed; }
    public float JumpForce { get => jumpForce; }
    public int MaxJumpCount { get => maxJumpCount; }
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

    public void ChangeSpeed(float value)
    {
        moveSpeed += value;
    }
    
    public void ChangeJumpForce(float value)
    {
        jumpForce += value;
    }

    public void ChangeJumpCount(float value)
    {
        maxJumpCount += (int)value;
    }

    void Die()
    {
        isDie = true;
    }
}
