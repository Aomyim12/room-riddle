using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [Header("Pair Setup")]
    public string pairID;
    public string hintText;
    public Image slotImage;
    public TextMeshProUGUI labelText;

    [HideInInspector] public DragItem matchedItem;

    void Start()
    {
        if (labelText != null)
            labelText.text = hintText;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DragItem>();

        if (dragged == null || dragged.isLocked) return; // ?? ถ้าล็อกแล้ว ไม่รับ Drop

        if (dragged.pairID == pairID)
        {
            Debug.Log("? Correct Match: " + pairID);
            slotImage.color = new Color(0.6f, 1f, 0.6f);

            // ตั้ง parent ใหม่ให้ item อยู่ใต้ slot
            dragged.transform.SetParent(transform);

            // ล็อกให้อยู่ตรงกลางช่องแบบแน่นอน
            RectTransform draggedRect = dragged.GetComponent<RectTransform>();
            RectTransform slotRect = GetComponent<RectTransform>();

            // รีเซ็ตค่า anchor และ pivot ให้ตรงกลาง
            draggedRect.anchorMin = new Vector2(0.5f, 0.5f);
            draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
            draggedRect.pivot = new Vector2(0.5f, 0.5f);
            // ?? ล็อกไอเท็มไว้ในช่อง
            dragged.transform.SetParent(transform);
            dragged.GetComponent<RectTransform>().anchoredPosition = Vector2.up;
            dragged.isLocked = true;

            dragged.GetComponent<CanvasGroup>().blocksRaycasts = false;
            matchedItem = dragged;
        }
        else
        {
            Debug.Log("? Wrong Match!");
            slotImage.color = new Color(1f, 0.6f, 0.6f);
        }
    }
}
