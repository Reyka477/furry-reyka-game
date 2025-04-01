using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class StartBattle : MonoBehaviour
    {
        public Button battleButton;
        public TMP_Text buttonText;
        public Sprite startSprite;
        public Sprite stopSprite;
        public Image buttonImage;
        public BattleManager battleManager;

        private bool _isStarted = false;

        void Start()
        {
            battleButton.onClick.AddListener(ToggleButton);
        }

        void ToggleButton()
        {
            _isStarted = !_isStarted;

            if (_isStarted)
            {
                battleManager.FindHeroesInSlots(); // ищем всех героев

                if (battleManager.heroes.Count > 0)
                {
                    buttonText.text = "Stop";
                    buttonImage.sprite = stopSprite;
                    battleManager.StartBattle();
                }
                else
                {
                    Debug.LogWarning("Нет героев в слотах. Бой не начат.");
                    _isStarted = false; // сбрасываем флаг
                }
            }
            else
            {
                buttonText.text = "Start";
                buttonImage.sprite = startSprite;
                battleManager.StopBattle();
            }
        }
    }
}