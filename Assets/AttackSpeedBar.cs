using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class AttackSpeedBar : MonoBehaviour
{
    public Image fillImage; // Полоска задержки перед атакой
    private float attackInterval; // Время полного отката
    private float currentCooldown; // Оставшееся время до атаки

    // Устанавливаем скорость атаки (чем больше attackSpeed, тем быстрее перезарядка)
    public void SetAttackSpeed(float attackSpeed)
    {
        attackInterval = 1f / attackSpeed; // Например, если attackSpeed = 2, то откат = 0.5 сек
        currentCooldown = attackInterval;
    }

    private void Update()
    {
        // Уменьшаем время отката
        currentCooldown -= Time.deltaTime;

        // Обновляем заполненность полоски
        fillImage.fillAmount = currentCooldown / attackInterval;

        // Если откат завершился, снова запускаем цикл атаки
        if (currentCooldown <= 0)
        {
            currentCooldown = attackInterval;
        }
    }
}