using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public string itemID; // �к� ID �ͧ�ѵ�ع��
    public Transform originalParent; // �纵��˹�������

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
        transform.SetParent(transform.root); // ����价�� Canvas

        canvasGroup.alpha = 0.6f; // ���������ʧ����ҡ
        canvasGroup.blocksRaycasts = false; // �Դ��õ�Ǩ�Ѻ����ҡ
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // �׹��Ҥ����ֺ�ʧ
        canvasGroup.blocksRaycasts = true; // �Դ��õ�Ǩ�Ѻ�ա����

        // ����ҧ���١��� ����Ѻ仵��˹����
        if (transform.parent == originalParent || transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}