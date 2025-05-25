using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuickQuizRoom : MonoBehaviour
{
    [Header("References")]
    public TMP_Text questionText;
    public TMP_Text timerText;
    public TMP_Text roundText;
    public Button[] answerButtons;

    private float timeLeft = 30f;
    private int roomScore = 0;

    private void Start()
    {
        SetupQuestion();
        UpdateUI();
    }

    private void SetupQuestion()
    {
        // ตัวอย่างคำถาม (ในเกมจริงควรโหลดจากระบบคำถาม)
        questionText.text = "5 x 5 เท่ากับเท่าไร?";

        string[] options = { "10", "25", "30", "15" };
        int correctIndex = 1;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = options[i];
            int index = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index, index == correctIndex));
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = $"เวลาเหลือ: {Mathf.RoundToInt(timeLeft)}";

        if (timeLeft <= 0)
        {
            EndRoom();
        }
    }

    private void OnAnswerSelected(int answerIndex, bool isCorrect)
    {
        if (isCorrect)
        {
            roomScore = Mathf.RoundToInt(timeLeft) * 2;
            GameManager.Instance.AddScore(roomScore);
            answerButtons[answerIndex].image.color = Color.green;
        }
        else
        {
            answerButtons[answerIndex].image.color = Color.red;
        }

        Invoke("EndRoom", 1f);
    }

    private void UpdateUI()
    {
        roundText.text = $"รอบ {GameManager.Instance.currentRound}";
    }

    private void EndRoom()
    {
        GameManager.Instance.LoadNextRoom();
    }
}