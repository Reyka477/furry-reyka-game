using System.Collections;
using UnityEngine;

namespace Script
{
    public class BattleManager : MonoBehaviour
    {
        public Character hero; // Ссылка на героя
        public Monster monster; // Ссылка на монстра

        public void StartBattle()
        {
            // Подписываемся на события атаки персонажа и монстра
            hero.attackSpeedBar.onAttack = HeroAttack;
            monster.attackSpeedBar.onAttack = MonsterAttack;
            // Запускаем AttackSpeedBar
            hero.attackSpeedBar.isFighting = true;
            monster.attackSpeedBar.isFighting = true;
        }

        public void StopBattle()
        {
            hero.attackSpeedBar.isFighting = false;
            monster.attackSpeedBar.isFighting = false;
        }

        public void FindHeroInSlots()
        {
            // Находим все объекты с тегом "Slot"
            GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

            foreach (GameObject slot in slots)
            {
                // Проверяем, есть ли у слота дочерние объекты
                foreach (Transform child in slot.transform)
                {
                    // Проверяем, есть ли у дочернего объекта тег "Hero"
                    if (child.CompareTag("Hero"))
                    {
                        hero = child.GetComponent<Character>();
                        Debug.Log("Герой найден и установлен: " + hero.name);
                        return;
                    }
                }
            }

            Debug.LogWarning("Герой в слотах не найден!");
        }

        private void HeroAttack()
        {
            if (monster.currentHp > 0)
            {
                monster.GetDamage(hero.DamageCalculation());
            }

            // Проверяем, умер ли монстр
            if (monster.currentHp <= 0)
            {
                StopBattle();
                monster.Die();
                StartCoroutine(MonsterRespawn());
            }
        }

        private void MonsterAttack()
        {
            if (hero.currentHp > 0)
            {
                hero.GetDamage(monster.DamageCalculation());
            }

            // Проверяем, умер ли герой
            if (hero.currentHp <= 0)
            {
                hero.Die();
            }
        }

        public IEnumerator MonsterRespawn()
        {
            // Ждем 3 секунды
            yield return new WaitForSeconds(3f);

            // Возвращаем спрайт в нормальное состояние
            monster.spriteRenderer.color = monster.originalColor;

            // Восстанавливаем здоровье и обновляем UI
            monster.currentHp = monster.maxHealth;
            monster.healthBar.SetHealth(monster.currentHp);
            monster.attackSpeedBar.isAlive = true;

            // Начинаем бой заново
            StartBattle();
        }
    }
}