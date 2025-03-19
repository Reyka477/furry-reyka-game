using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [Header("Основные параметры")]
    public string monsterName;
    public int level = 1;
    public int experience = 10;
    
    [Header("Характеристики")]
    public int maxHealth = 100;
    public int currentHp;
    public int def = 5;
    public int attack = 2;
    public float attackSpeed = 1f;
    public int critChance = 20;
    public float critDamage = 1.5f; //сила крита %
    
    [Header("UI элементы")]
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

    public int DamageCalculation()
    {
        int damage = 0;
        
        if (Random.Range(0, 100) < critChance)
        {
            damage = Mathf.RoundToInt(attack * critDamage);
        }
        else
        {
            damage = attack;
        }
        return damage;
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
        // Выдает экспу
    }
}