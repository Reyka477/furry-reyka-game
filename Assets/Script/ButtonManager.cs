using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button[] buttons; // Массив всех кнопок
    private Color originalColor; // Оригинальный цвет кнопки

    public void OnButtonClicked(Button clickedButton)
    {
        // Пробегаем по всем кнопкам и восстанавливаем их цвет
        foreach (Button button in buttons)
        {
            // Если кнопка не та, на которую кликнули, восстанавливаем её цвет
            if (button != clickedButton)
            {
                button.GetComponent<Image>().color = originalColor;
            }
        }

        // Затемняем выбранную кнопку (при клике)
        clickedButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);

        Debug.Log("Кнопка нажата: " + clickedButton.name);
    }
}