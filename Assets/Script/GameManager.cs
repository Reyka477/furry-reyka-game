using System.Collections;
using TMPro;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public float autosaveInterval = 10f; // 5 минут
        public TMP_Text saveNotification; // UI-текст "Прогресс сохранён"
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            StartCoroutine(AutoSaveLoop());
        }

        private IEnumerator AutoSaveLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(autosaveInterval);
                SaveProgress();
            }
        }

        private void ShowSaveNotification()
        {
            if (saveNotification != null)
            {
                saveNotification.text = "Прогресс сохранён";
            }
        }

        private IEnumerator HideNotificationAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        public void SaveProgress()
        {
            GameSaveData data = PlayerProgress.Instance.GetSaveData();
            SaveSystem.SaveGame(data);
        }

        public void LoadProgress()
        {
            var data = SaveSystem.LoadGame();
            if (data != null)
            {
                PlayerProgress.Instance.LoadFromSave(data);
                Debug.Log("Прогресс загружен");
            }
        }
    }
}