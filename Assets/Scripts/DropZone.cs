using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string expectedItemID; // ID �ͧ�ѵ�ط�����ҧ�����
    public DraggableItem matchedItem; // ���ѵ�ط���ҧ�١��ͧ

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            if (droppedItem.itemID == expectedItemID)
            {
                // �Ѻ���١��ͧ
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.localPosition = Vector3.zero;
                matchedItem = droppedItem;

                // �Դ����ҡ��ҵ�ͧ���
                droppedItem.enabled = false;

                Debug.Log("�Ѻ���١��ͧ!");
            }
            else
            {
                // �Ѻ���Դ
                Debug.Log("�Ѻ���Դ!");

                // ��Ѻ仵��˹����
                droppedItem.transform.SetParent(droppedItem.originalParent);
                droppedItem.transform.localPosition = Vector3.zero;
            }
        }
    }
}