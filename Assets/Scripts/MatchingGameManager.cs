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
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0)
        {
            EndGame("Time Up!");
        }

        if (AllMatched())
        {
            EndGame("All Matched!");
        }
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

    void EndGame(string reason)
    {
        gameEnded = true;
        timerText.text = reason;
        Invoke(nameof(ClosePanel), 2f);   // หน่วง 2 วิแล้วค่อยปิด panel
    }

    void ClosePanel()
    {
        panelToClose.SetActive(false);    // ปิด Panel
    }

}
