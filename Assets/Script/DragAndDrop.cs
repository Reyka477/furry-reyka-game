using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false; // Флаг, указывающий, что объект перетаскивается
    private Vector2 offset; // На сколько клик по объекту смещен от левого верхнего края
    private Vector2 startPosition;

    void OnMouseDown()
    {
        startPosition = transform.position;
        // Когда нажимаем на объект, начинаем перетаскивание
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Перемещаем объект вместе с курсором мыши
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position =
                new Vector2(mousePosition.x + offset.x, mousePosition.y + offset.y);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Получаем ВСЕ коллайдеры в точке, куда отпущен персонаж
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Slot")) 
            {
                // Если найден слот, фиксируем позицию в слоте и выходим из метода
                transform.position = collider.transform.position;
                return;
            }
        }

        // Если слота нет, возвращаем персонажа на стартовую позицию
        transform.position = startPosition;
    }
}