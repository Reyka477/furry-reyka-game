using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCharacter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentAfterDrag;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent; // Запоминаем родителя
        transform.SetParent(transform.root); // Выносим на верхний уровень
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Перемещение за курсором
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot")) 
        {
            transform.SetParent(eventData.pointerEnter.transform, false); // Привязываем к слоту
        }
        else
        {
            transform.SetParent(parentAfterDrag); // Возвращаем, если не попали в слот
        }
    }
}