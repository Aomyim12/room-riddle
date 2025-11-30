using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchingGameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float timeLimit = 60f; // เวลาเริ่มต้น (วินาที)
    private float timer;
    private bool gameEnded = false;

    [Header("UI")]
    public TMP_Text timerText; // ตัวแสดงเวลา
    public GameObject panelToClose;

    [Header("Game Elements")]
    public DropSlot[] dropSlots; // DropSlot ทั้งหมดในฉาก

    void Start()
    {
        timer = timeLimit;
        gameEnded = true;   // เกมเริ่มในสถานะหยุด รอเปิด Panel
        UpdateTimerUI();
    }


    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0)
        {
            EndGame("Time Up!", false); // เวลาหมด: ต้องกลับเมนู
        }

        if (AllMatched())
        {
            EndGame("All Matched!", true); // ชนะ: ต้องปิด Panel และหยุดการกด
        }
    }

    public void StartGame()
    {
        if (!gameEnded) return; // ถ้าเกมยังไม่จบ ไม่ต้องเริ่มใหม่

        timer = timeLimit;
        gameEnded = false; // เริ่มนับเวลาใน Update()
        UpdateTimerUI();

        // ควรเพิ่มการรีเซ็ต DropSlot และ Element อื่นๆ ของเกมที่นี่
        Debug.Log("Game Timer Started!");
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.CeilToInt(timer)}";
        }
    }

    bool AllMatched()
    {
        foreach (var zone in dropSlots)
        {
            if (zone == null || zone.matchedItem == null)
                return false;
        }
        return true;
    }

    void EndGame(string reason, bool winGame)
    {
        gameEnded = true;
        timerText.text = reason;

        if (winGame)
        {
            // หน่วง 2 วิแล้วค่อยปิด Panel และหยุดการโต้ตอบ
            Invoke(nameof(ClosePanel), 2f);
        }
        else // เวลาหมด
        {
            // หน่วง 2 วิแล้วค่อยโหลด Scene เมนู
            Invoke(nameof(LoadMenuScene), 2f);
        }
    }

    void ClosePanel()
    {
        panelToClose.SetActive(false);    // ปิด Panel
    }
    void LoadMenuScene()
    {
        // ต้องแน่ใจว่าได้เพิ่ม Scene ที่ชื่อ "MainMenu" ไว้ใน Build Settings แล้ว
        SceneManager.LoadScene("Menu");
    }
    public void OnPanelOpened()
    {
        StartGame();
    }
}
