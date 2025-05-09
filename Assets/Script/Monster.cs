using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Monster : MonoBehaviour
    {
        [Header("Основные параметры")] public string monsterName;
        public int level = 1;
        public int experience = 10;

        [Header("Характеристики")] public int maxHealth = 100;
        public int currentHp;
        public int def = 5;
        public int attack = 2;
        public float attackSpeed = 1f;
        public int critChance = 20;
        public float critDamage = 1.5f; //сила крита %

        [Header("UI элементы")] public Dictionary<int, float> Drop = new Dictionary<int, float>()
        {
            { 1, 0.5f },
            { 2, 0.9f },
            { 3, 0.2f },
            { 4, 0.9f }
        };

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

        public List<int> DropCalculation()
        {
            List<int> dropItems = new List<int>();

            foreach (var item in Drop)
            {
                if (Random.value <= item.Value)
                {
                    dropItems.Add(item.Key);
                    Debug.Log(item.Key);
                }
            }

            return dropItems;
        }

        public List<int> Die()
        {
            // Переключаем полоску на режим "умер" чтоб обнулить её
            attackSpeedBar.isAlive = false;

            // Делаем спрайт серым
            spriteRenderer.color = new Color(0.28f, 0.28f, 0.28f, 1f);

            // Выдает экспу
            if (PlayerProgress.Instance != null)
            {
                PlayerProgress.Instance.AddExperience(experience);
            }
            else
            {
                Debug.LogWarning("PlayerProgress.Instance = null");
            }

            Debug.Log("Монстр умер, опыт должен быть начислен.");
            
            return DropCalculation();
        }
    }
}