using UnityEngine;
using UnityEngine.UI;

public class OpenPanelOnClick : MonoBehaviour
{
    [Header("Panel To Open")]
    public GameObject targetPanel;     // Panel ที่ต้องการเปิด
    public float delayBeforeOpen = 0.5f;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        button.interactable = false;
        Invoke(nameof(OpenPanel), delayBeforeOpen);
    }

    private void OpenPanel()
    {
        targetPanel.SetActive(true);
        button.interactable = true;   // เปิดให้คลิกอีกครั้ง (ถ้าต้องการ)
    }
}
