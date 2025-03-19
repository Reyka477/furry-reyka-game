using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image spriteRenderer; // Ссылка на спрайт монстра

    public Color originalColor; // Исходный цвет спрайта

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
        // Переключаем полоску на режим "умер" чтоб обнулить её
        attackSpeedBar.isAlive = false;

        // Делаем спрайт серым
        spriteRenderer.color = new Color(0.28f, 0.28f, 0.28f, 1f);

        // Выдаем лут
    }
}