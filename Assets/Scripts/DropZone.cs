using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string expectedAnswer;
    public TMP_Text labelText;
    private DragDropRoom roomController;

    private void Start()
    {
        roomController = FindObjectOfType<DragDropRoom>();
        labelText = GetComponentInChildren<TMP_Text>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggedItem != null)
        {
            if (draggedItem.correctAnswer == expectedAnswer)
            {
                draggedItem.transform.SetParent(transform);
                draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                roomController.OnCorrectMatch();
            }
            else
            {
                draggedItem.GetComponent<RectTransform>().anchoredPosition = draggedItem.GetComponent<DraggableItem>().originalPosition;
            }
        }
    }
}