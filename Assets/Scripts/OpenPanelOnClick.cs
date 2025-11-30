using UnityEngine;
using UnityEngine.UI;

public class OpenPanelOnClick : MonoBehaviour
{
    [Header("Panel To Open")]
    public GameObject targetPanel;    // Panel ที่ต้องการเปิด
    public float delayBeforeOpen = 0.5f;

    private Button button;
    private bool hasBeenClicked = false; // ตัวแปรสำหรับตรวจสอบสถานะการคลิก

    private void Start()
    {
        button = GetComponent<Button>();

        // ตรวจสอบว่ามี Button Component อยู่
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("OpenPanelOnClick requires a Button component on the same GameObject.");
            enabled = false; // ปิด Script ถ้าไม่มี Button
        }
    }

    private void OnButtonClicked()
    {
        // 1. ป้องกันการคลิกซ้ำทันที
        if (hasBeenClicked) return;

        // 2. ตั้งค่าสถานะว่าคลิกไปแล้ว และปิดปุ่มอย่างถาวร
        hasBeenClicked = true;
        button.interactable = false;

        // 3. หน่วงเวลาแล้วจึงเปิด Panel
        Invoke(nameof(OpenPanel), delayBeforeOpen);
    }

    private void OpenPanel()
    {
        // เปิด Panel
        targetPanel.SetActive(true);
    }
}