using UnityEngine;

namespace Script
{
    public class Character : MonoBehaviour
    {
        [Header("Основные параметры")]
        public string characterName;
        public int level = 1;
        public int maxLevel = 100;
    
        [Header("Характеристики")]
        public int maxHealth = 100;
        public int currentHp;
        public int def = 5;
        public int attack = 2;
        public float attackSpeed = 1.0f;
        public int critChance = 20;
        public float critDamage = 1.5f;
        public int attackDistance = 1;
    
        [Header("UI элементы")]
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

        public void LvlUp()
        {
        
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
}