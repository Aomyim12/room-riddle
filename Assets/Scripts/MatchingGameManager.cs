using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchingGameManager : MonoBehaviour
{
    public float timeLimit = 60f; // ระยะเวลาเริ่มต้น (วินาที)
    private float timer;
    private bool gameEnded = false;

    public TMP_Text timerText; // UI แสดงเวลา
    public DropZone[] dropZones; // DropZone ทั้งหมดในฉาก

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
            EndGame();
        }

        if (AllMatched())
        {
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
        }
    }

    bool AllMatched()
    {
        foreach (var zone in dropZones)
        {
            if (zone.matchedItem == null)
                return false;
        }
        return true;
    }

    void EndGame()
    {
        gameEnded = true;
        SceneManager.LoadScene("ChooseRoom");
    }
}
