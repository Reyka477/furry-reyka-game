using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalButtonManager : MonoBehaviour
{
    [Header("Все группы вкладок")]
    public List<TabGroup> tabGroups;

    private Dictionary<Button, GameObject> buttonToPanel = new();
    private Dictionary<Button, TabGroup> buttonToGroup = new();
    private Dictionary<TabGroup, Button> activeButtons = new();
    private Dictionary<Button, Color> originalColors = new();

    [System.Serializable]
    public class TabGroup
    {
        public string groupName;
        public List<Button> buttons;
        public List<GameObject> panels;

        [Header("Цвета для кнопок")]
        public Color selectedColor = new Color(0.6f, 0.6f, 0.6f);
    }

    void Start()
    {
        foreach (TabGroup group in tabGroups)
        {
            for (int i = 0; i < group.buttons.Count && i < group.panels.Count; i++)
            {
                var btn = group.buttons[i];
                var panel = group.panels[i];

                if (btn == null || panel == null) continue;

                if (!buttonToPanel.ContainsKey(btn))
                {
                    buttonToPanel.Add(btn, panel);
                    buttonToGroup.Add(btn, group);

                    // Сохраняем изначальный цвет
                    if (btn.TryGetComponent(out Image img) && !originalColors.ContainsKey(btn))
                        originalColors.Add(btn, img.color);

                    btn.onClick.AddListener(() => OnTabClicked(btn));
                }
            }
        }
    }

    public void OnTabClicked(Button clickedButton)
    {
        if (!buttonToGroup.TryGetValue(clickedButton, out TabGroup group)) return;

        // Скрываем все панели в группе
        foreach (var panel in group.panels)
        {
            if (panel != null) panel.SetActive(false);
        }

        // Возвращаем предыдущей кнопке её цвет
        if (activeButtons.TryGetValue(group, out Button previousButton))
        {
            if (originalColors.TryGetValue(previousButton, out Color original))
                SetButtonColor(previousButton, original);
        }

        // Показываем нужную панель
        if (buttonToPanel.TryGetValue(clickedButton, out GameObject panelToOpen) && panelToOpen != null)
            panelToOpen.SetActive(true);

        // Подсвечиваем активную кнопку
        if (originalColors.TryGetValue(clickedButton, out Color origColor))
        {
            SetButtonColor(clickedButton, DarkenColor(origColor, 0.6f)); // затемнение на 40%
        }

        // Обновляем активную кнопку в группе
        activeButtons[group] = clickedButton;
    }

    private void SetButtonColor(Button button, Color color)
    {
        if (button != null && button.TryGetComponent(out Image img))
            img.color = color;
    }
    private Color DarkenColor(Color color, float factor)
    {
        return new Color(color.r * factor, color.g * factor, color.b * factor, color.a);
    }

}
