using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth = 100;
    public int currentHp;
    public int def = 5;
    public int attack = 2;
    public float attackSpeed = 1.0f;
    public int critChance = 20;
    public double critDamage = 1.5;
    public int attackDistance;
    public HealthBar healthBar;
    public AttackSpeedBar attackSpeedBar;

    public void Awake()
    {
        currentHp = maxHealth;
        attackSpeedBar.SetAttackSpeed(attackSpeed);
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Rename(string newName)
    {
        characterName = newName;
    }

    public void DamageCalculation()
    {
    }

    public void GetDamage(int damage)
    {
        if (damage <= def)
        {
            this.currentHp--;
        }
        else
        {
            this.currentHp -= (damage - this.def);
        }

        if (this.currentHp <= 0)
        {
            this.currentHp = 0;
            this.Die();
        }

        healthBar.SetHealth(currentHp);
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        // Делаем чтобы хилл не превышал максимальное ХП
        if (currentHp > maxHealth) currentHp = maxHealth;
        healthBar.SetHealth(currentHp);
    }

    public void Die()
    {
    }
}