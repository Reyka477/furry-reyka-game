using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Script
{
    public class HealthBar : MonoBehaviour
    {
        public Image fillImage;
        public int maxHealth;

        public void SetMaxHealth(int health)
        {
            maxHealth = health;
            SetHealth(health); // Обновляем текущее HP, чтобы полоска была полной
        }

        // Обновляем текущее здоровье
        public void SetHealth(int health)
        {
            fillImage.fillAmount = (float)health / maxHealth; // Вычисляем процент заполнения
            
            // TODO чтобы над полоской здоровья всплывал серый текст с значением полученого урона
        }
    }
}