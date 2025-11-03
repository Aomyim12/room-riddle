using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Pair Setup")]
    public string pairID;
    public TextMeshProUGUI labelText;

    [HideInInspector] public bool isLocked = false; // ? เมื่อจับคู่แล้วจะล็อกไว้

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Start()
    {
        if (labelText != null)
            labelText.text = pairID;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return; // ?? ถ้าล็อกไว้แล้ว ห้ามลากอีก

        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return; // ?? ห้ามลากถ้าล็อก
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return; // ?? ห้ามปล่อยถ้าล็อกแล้ว

        // ถ้า Drop ไม่ถูกที่ -> กลับที่เดิม
        rectTransform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
