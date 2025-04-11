using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class BattleManager : MonoBehaviour
    {
        public Monster monster;
        public List<Character> heroes = new List<Character>();

        private bool battleIsActive = false;

        public bool IsBattleActive => battleIsActive;

        public void StartBattle()
        {
            FindHeroesInSlots();

            if (heroes.Count == 0)
            {
                Debug.LogError("Нет героев для начала боя!");
                return;
            }

            battleIsActive = true;

            foreach (var hero in heroes)
            {
                hero.attackSpeedBar.onAttack = () => HeroAttack(hero);
                hero.attackSpeedBar.isFighting = true;
            }

            monster.attackSpeedBar.onAttack = MonsterAttack;
            monster.attackSpeedBar.isFighting = true;
        }

        public void StopBattle()
        {
            battleIsActive = false;

            foreach (var hero in heroes)
            {
                hero.attackSpeedBar.isFighting = false;
            }

            monster.attackSpeedBar.isFighting = false;
        }

        public void FindHeroesInSlots()
        {
            heroes.Clear();

            foreach (SlotArena slot in GetComponentsInChildren<SlotArena>())
            {
                foreach (Transform child in slot.transform)
                {
                    if (child.CompareTag("Hero"))
                    {
                        Character hero = child.GetComponent<Character>();
                        if (hero != null)
                        {
                            heroes.Add(hero);
                            Debug.Log($"Герой {hero.name} добавлен на арену {name}");
                        }
                    }
                }
            }
        }

        private void HeroAttack(Character hero)
        {
            if (!battleIsActive) return;

            if (monster.currentHp > 0)
            {
                monster.GetDamage(hero.DamageCalculation());
            }

            if (monster.currentHp <= 0)
            {
                monster.Die();
                StopBattle();
                StartCoroutine(MonsterRespawn());
            }
        }

        private void MonsterAttack()
        {
            if (!battleIsActive) return;

            foreach (var hero in heroes)
            {
                if (hero.currentHp > 0)
                {
                    hero.GetDamage(monster.DamageCalculation());
                    if (hero.currentHp <= 0)
                    {
                        hero.Die();
                        CheckAllHeroesDead();
                    }
                    return;
                }
            }

            CheckAllHeroesDead();
        }

        private void CheckAllHeroesDead()
        {
            foreach (var hero in heroes)
            {
                if (hero.currentHp > 0)
                {
                    return;
                }
            }

            Debug.Log("Все герои погибли!");
            StopBattle();
        }

        public IEnumerator MonsterRespawn()
        {
            yield return new WaitForSeconds(3f);

            monster.spriteRenderer.color = monster.originalColor;
            monster.currentHp = monster.maxHealth;
            monster.healthBar.SetHealth(monster.currentHp);
            monster.attackSpeedBar.isAlive = true;

            // ⬇️ добавь перед StartBattle()
            FindHeroesInSlots();

            StartBattle();
        }
    }
}
