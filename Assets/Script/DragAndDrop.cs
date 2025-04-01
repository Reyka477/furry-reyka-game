using UnityEngine;
using UnityEngine.EventSystems;
using Script;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Character character = GetComponent<Character>();

        if (character != null && character.currentBattleArena != null && character.currentBattleArena.IsBattleActive)
        {
            Debug.Log("Нельзя перетаскивать во время боя");
            return;
        }

        startPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;

        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Character character = GetComponent<Character>();

        if (character != null && character.currentBattleArena != null && character.currentBattleArena.IsBattleActive)
        {
            return;
        }

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Character character = GetComponent<Character>();

        if (character != null && character.currentBattleArena != null && character.currentBattleArena.IsBattleActive)
        {
            return;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        bool slotFound = false;

        foreach (var hoveredObject in eventData.hovered)
        {
            if (hoveredObject.CompareTag("Slot"))
            {
                SlotArena slotArena = hoveredObject.GetComponent<SlotArena>();

                if (slotArena != null && slotArena.battleArena != null && slotArena.battleArena.IsBattleActive)
                {
                    Debug.Log("Нельзя класть героя в слот на арене, где уже идет бой");
                    break;
                }

                rectTransform.SetParent(hoveredObject.transform, false);
                rectTransform.anchoredPosition = Vector2.zero;

                if (slotArena != null && character != null)
                {
                    if (character.currentBattleArena != null && character.currentBattleArena != slotArena.battleArena)
                    {
                        character.currentBattleArena.heroes.Remove(character);
                    }

                    character.currentBattleArena = slotArena.battleArena;
                }

                slotFound = true;
                break;
            }
        }

        if (!slotFound)
        {
            rectTransform.SetParent(originalParent, false);
            rectTransform.anchoredPosition = startPosition;
        }
    }
}