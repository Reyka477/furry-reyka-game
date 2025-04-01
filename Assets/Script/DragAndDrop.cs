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
        startPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;

        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;

        // Если начался перетаскивание героя, останавливаем текущий бой на его арене
        Character character = GetComponent<Character>();
        if (character.currentBattleArena != null)
        {
            character.currentBattleArena.StopBattle();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        bool slotFound = false;

        foreach (var hoveredObject in eventData.hovered)
        {
            if (hoveredObject.CompareTag("Slot"))
            {
                rectTransform.SetParent(hoveredObject.transform, false);
                rectTransform.anchoredPosition = Vector2.zero;

                SlotArena slotArena = hoveredObject.GetComponent<SlotArena>();
                if (slotArena != null)
                {
                    Character character = GetComponent<Character>();

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
