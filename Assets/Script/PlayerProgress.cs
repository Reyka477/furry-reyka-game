using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress Instance;

    public int playerLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;

    [Header("UI Elements")] public Image expBar;
    public TMP_Text levelText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            playerLevel++;

            // Увеличиваем требования на след. уровень
            expToNextLevel = CalculateExpForNextLevel(playerLevel);
        }

        UpdateUI();
    }

    private int CalculateExpForNextLevel(int level)
    {
        // Пример: экспоненциальный рост
        return 100 + (level - 1) * 50;
    }

    private void UpdateUI()
    {
        if (expBar != null)
        {
            expBar.fillAmount = (float)currentExp / expToNextLevel;
        }

        if (levelText != null)
        {
            levelText.text = $"Level {playerLevel}";
        }
    }
}