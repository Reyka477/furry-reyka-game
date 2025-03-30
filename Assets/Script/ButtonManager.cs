using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button[] allButtons; // Массив для всех кнопок

    private Button activeBiomeButton;
    private Button activeMenuButton;

    private Color originalColor; // Исходный цвет кнопок
    private float darkeningFactor = 0.6f; // Множитель для затемнения (0 - полная темнота, 1 - без изменений)

    void Start()
    {
        // Получаем все кнопки, которые находятся в дочерних объектах этого объекта
        allButtons = GetComponentsInChildren<Button>();

        if (allButtons.Length > 0)
            originalColor = allButtons[0].GetComponent<Image>().color;

        // Добавляем обработчики событий для всех кнопок
        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    public void OnButtonClicked(Button clickedButton)
    {
        Transform buttonParent = clickedButton.transform.parent; // Получаем родителя кнопки

        if (buttonParent != null) // Проверяем, есть ли у кнопки родитель
        {
            if (buttonParent.name == "BiomesButtons") // Если кнопка из Biomes
            {
                // Сбрасываем цвет с предыдущей активной кнопки в Biomes, если она существует
                if (activeBiomeButton != null && activeBiomeButton != clickedButton)
                {
                    activeBiomeButton.GetComponent<Image>().color = originalColor;
                }

                activeBiomeButton = clickedButton;
                activeBiomeButton.GetComponent<Image>().color =
                    DarkenColor(originalColor); // Заменяем цвет на затемненный
            }
            else if (buttonParent.name == "MenuButtons") // Если кнопка из Menu
            {
                // Сбрасываем цвет с предыдущей активной кнопки в Menu, если она существует
                if (activeMenuButton != null && activeMenuButton != clickedButton)
                {
                    activeMenuButton.GetComponent<Image>().color = originalColor;
                }

                activeMenuButton = clickedButton;
                activeMenuButton.GetComponent<Image>().color =
                    DarkenColor(originalColor); // Заменяем цвет на затемненный
            }
        }
    }

    // Метод для затемнения цвета
    private Color DarkenColor(Color color)
    {
        return new Color(color.r * darkeningFactor, color.g * darkeningFactor, color.b * darkeningFactor);
    }
}