using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public string itemID; // ระบุ ID ของวัตถุนี้
    public Transform originalParent; // เก็บตำแหน่งเดิมไว้

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root); // ย้ายไปที่ Canvas

        canvasGroup.alpha = 0.6f; // ทำให้โปร่งแสงขณะลาก
        canvasGroup.blocksRaycasts = false; // ปิดการตรวจจับขณะลาก
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // คืนค่าความทึบแสง
        canvasGroup.blocksRaycasts = true; // เปิดการตรวจจับอีกครั้ง

        // ถ้าวางไม่ถูกที่ ให้กลับไปตำแหน่งเดิม
        if (transform.parent == originalParent || transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}