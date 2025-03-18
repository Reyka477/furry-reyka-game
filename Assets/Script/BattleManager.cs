using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Character hero; // Ссылка на героя
    public Monster monster; // Ссылка на монстра

    public void StartBattle()
    {
        // Подписываемся на события атаки персонажа и монстра
        hero.attackSpeedBar.onAttack += HeroAttack;
        monster.attackSpeedBar.onAttack += MonsterAttack;
        // Запускаем AttackSpeedBar
        hero.attackSpeedBar.isFighting = true;
        monster.attackSpeedBar.isFighting = true;
    }

    public void StopBattle()
    {
        hero.attackSpeedBar.isFighting = false;
        monster.attackSpeedBar.isFighting = false;
    }

    private void HeroAttack()
    {
        if (monster.currentHp > 0)
        {
            monster.GetDamage(hero.attack);
        }

        // Проверяем, умер ли монстр
        if (monster.currentHp <= 0)
        {
            StopBattle();
            monster.Die();
        }
    }

    private void MonsterAttack()
    {
        if (hero.currentHp > 0)
        {
            hero.GetDamage(monster.attack);
        }

        // Проверяем, умер ли герой
        if (hero.currentHp <= 0)
        {
            hero.Die();
        }
    }

    public void MonsterRespawn()
    {
        // С задержкой в 3 секунды делаем монстру максимальное хп, начинаем бой заново
    }
}