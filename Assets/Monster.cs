using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public int maxHealth = 100;
    public int currentHp;
    public int def = 5;
    public int attack = 2;
    public float attackSpeed = 1f;
    public int critChance = 20;
    public double critDamage = 1.5; //сила крита %
    public Dictionary<int, int> Drop;
    public HealthBar healthBar;
    public AttackSpeedBar attackSpeedBar;

    public void Awake()
    {
        currentHp = maxHealth;
        attackSpeedBar.SetAttackSpeed(attackSpeed);
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Attack()
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
            this.Die();
        }

        healthBar.SetHealth(currentHp);
    }

    public void Die()
    {
        currentHp = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
}