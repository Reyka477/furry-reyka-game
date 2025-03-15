using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class AttackSpeedBar : MonoBehaviour
{
    public Image fillImage; // Полоска задержки перед атакой
    private float maxCooldown; // Время полного отката
    private float currentCooldown; // Оставшееся время до атаки
    private bool isReloading = false; // Идет ли перезарядка

    // Устанавливаем скорость атаки (чем больше attackSpeed, тем быстрее перезарядка)
    public void SetAttackSpeed(float attackSpeed)
    {
        maxCooldown = 1f / attackSpeed; // Например, если attackSpeed = 2, то откат = 0.5 сек
        RestartCooldown(); // Запускаем первый цикл перезарядки
    }

    // Начинаем новый цикл отката
    public void RestartCooldown()
    {
        currentCooldown = maxCooldown;
        isReloading = true;
    }

    private void Update()
    {
        if (!isReloading) return;

        // Уменьшаем время отката
        currentCooldown -= Time.deltaTime;

        // Обновляем заполненность полоски
        fillImage.fillAmount = currentCooldown / maxCooldown;

        // Если откат завершился, снова запускаем цикл атаки
        if (currentCooldown <= 0)
        {
            currentCooldown = 0;
            isReloading = false;
            RestartCooldown();
        }
    }
}