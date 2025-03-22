using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class SlotManager : MonoBehaviour
    {
        public static SlotManager Instance; // Синглтон для удобного доступа
        public List<GameObject> characterSlots = new List<GameObject>(); // Список слотов

        private void Awake()
        {
            Instance = this; // Инициализация синглтона
        }

        public void AssignCharacterToSlot(Character character, int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= characterSlots.Count)
            {
                Debug.LogWarning("Неверный индекс слота!");
                return;
            }

            // Устанавливаем персонажа в слот
            character.transform.SetParent(characterSlots[slotIndex].transform, false);
            character.transform.localPosition = Vector3.zero; // Выравниваем по слоту
        }
    }
}