using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class AttackSpeedBar : MonoBehaviour
    {
        public Image fillImage; // Полоска задержки перед атакой
        public float attackInterval; // Время полного отката
        public float attackPreparationTime; // Время подготовки атаки
        public Action onAttack;
        public bool isFighting = false;
        public bool isAlive = true;

        // Устанавливаем скорость атаки (чем больше attackSpeed, тем быстрее перезарядка)
        public void SetAttackSpeed(float attackSpeed)
        {
            attackInterval = 1f / attackSpeed; // Например, если attackSpeed = 2, то откат = 0.5 сек
            attackPreparationTime = 0;
        }

        private void Update()
        {
            if (!isAlive)
            {
                fillImage.fillAmount = 0;
                return;
            }

            if (isFighting)
            {
                // Увеличиваем время подготовки атаки
                attackPreparationTime += Time.deltaTime;

                // Обновляем заполненность полоски
                fillImage.fillAmount = attackPreparationTime / attackInterval;

                // Если полоска заполнилась, снова запускаем цикл атаки
                if (attackPreparationTime >= attackInterval)
                {
                    // Когда полоска заполнилась, должна выполнится атака
                    fillImage.fillAmount = 0;
                    attackPreparationTime = 0;
                    onAttack?.Invoke();
                }
            }
            else
            {
                fillImage.fillAmount = 1;
            }
        }
    }
}