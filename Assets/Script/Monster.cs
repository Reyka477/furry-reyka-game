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
    public SpriteRenderer spriteRenderer; // Ссылка на спрайт монстра

    private Color originalColor; // Исходный цвет спрайта

    public void Awake()
    {
        currentHp = maxHealth;
        attackSpeedBar.SetAttackSpeed(attackSpeed);
        healthBar.SetMaxHealth(maxHealth);
        originalColor = spriteRenderer.color; // Сохраняем оригинальный цвет
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
            currentHp = 0;
        }

        healthBar.SetHealth(currentHp);
    }

    public void Die()
    {
        // Делаем спрайт серым
        spriteRenderer.color = Color.gray;

        // Возвращаем спрайт в нормальное состояние
        spriteRenderer.color = originalColor;

        // Восстанавливаем здоровье и обновляем UI
        currentHp = maxHealth;
        healthBar.SetHealth(currentHp);
        // Возобновить бой
    }
}