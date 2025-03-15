using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class BattleManager : MonoBehaviour
    {
        public Monster monster;
        public Character hero;

        public void StartBattle()
        {
            monster.GetDamage(hero.attack);
            hero.GetDamage(monster.attack);
        }

        void Start()
        {
        }

        void Update()
        {
        }
    }
}