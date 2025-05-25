using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string expectedItemID; // ID ของวัตถุที่ควรวางที่นี่
    public DraggableItem matchedItem; // เก็บวัตถุที่วางถูกต้อง

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            if (droppedItem.itemID == expectedItemID)
            {
                // จับคู่ถูกต้อง
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.localPosition = Vector3.zero;
                matchedItem = droppedItem;

                // ปิดการลากถ้าต้องการ
                droppedItem.enabled = false;

                Debug.Log("จับคู่ถูกต้อง!");
            }
            else
            {
                // จับคู่ผิด
                Debug.Log("จับคู่ผิด!");

                // กลับไปตำแหน่งเดิม
                droppedItem.transform.SetParent(droppedItem.originalParent);
                droppedItem.transform.localPosition = Vector3.zero;
            }
        }
    }
}