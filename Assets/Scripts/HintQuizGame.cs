using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HintQuizGame : MonoBehaviour
{
    [System.Serializable]
    public class QuizItem
    {
        public string answer; // คำตอบที่ถูกต้อง
        public string[] hints; // ใบ้ 3 ข้อ
    }

    public List<QuizItem> quizItems = new List<QuizItem>();
    private QuizItem currentQuiz;

    public TMP_Text[] hintTexts; // Text สำหรับใบ้ 3 ข้อ
    public TMP_InputField answerInput;
    public Button submitButton;
    public TMP_Text resultText;
    public TMP_Text scoreText;
    public GameObject nextButton;

    private int currentHintIndex = 0;
    private int score = 0;
    private bool isAnswered = false;

    void Start()
    {
        // ตั้งค่าปุ่ม
        submitButton.onClick.AddListener(CheckAnswer);
        nextButton.SetActive(false);

        // เริ่มเกมด้วยคำถามแรก
        GetRandomQuestion();
    }

    void GetRandomQuestion()
    {
        if (quizItems.Count > 0)
        {
            // สุ่มคำถาม
            int randomIndex = Random.Range(0, quizItems.Count);
            currentQuiz = quizItems[randomIndex];

            // ลบคำถามที่ใช้แล้วออกจากลิสต์
            quizItems.RemoveAt(randomIndex);

            // รีเซ็ตสถานะ
            currentHintIndex = 0;
            isAnswered = false;
            answerInput.text = "";
            resultText.text = "";
            nextButton.SetActive(false);

            // แสดงใบ้แรก
            ShowHint(0);
        }
        else
        {
            // ถ้าคำถามหมด
            resultText.text = "เกมจบ! คะแนนรวม: " + score;
            submitButton.gameObject.SetActive(false);
            answerInput.gameObject.SetActive(false);
        }
    }

    void ShowHint(int index)
    {
        if (index < currentQuiz.hints.Length)
        {
            // แสดงใบ้ตามลำดับ
            for (int i = 0; i < hintTexts.Length; i++)
            {
                hintTexts[i].text = i <= index ? (i + 1) + ". " + currentQuiz.hints[i] : "";
            }
            currentHintIndex = index;
        }
    }

    public void CheckAnswer()
    {
        if (isAnswered) return;

        string userAnswer = answerInput.text.Trim().ToLower();
        string correctAnswer = currentQuiz.answer.ToLower();

        if (userAnswer == correctAnswer)
        {
            // ตอบถูก
            int points = (3 - currentHintIndex) * 10; // ได้คะแนนมากขึ้นถ้าตอบด้วยใบ้น้อยข้อ
            score += points;

            resultText.text = "ถูกต้อง! +" + points + " คะแนน";
            resultText.color = Color.green;
        }
        else
        {
            // ตอบผิด
            if (currentHintIndex < 2)
            {
                // แสดงใบ้เพิ่มถ้ายังมี
                ShowHint(currentHintIndex + 1);
                resultText.text = "ไม่ถูกต้อง ลองดูใบ้เพิ่ม!";
                resultText.color = Color.red;
                return;
            }
            else
            {
                // แสดงคำตอบถ้าใช้ใบ้ครบแล้ว
                resultText.text = "เสียใจด้วย! คำตอบคือ: " + currentQuiz.answer;
                resultText.color = Color.red;
            }
        }

        scoreText.text = "Score: " + score;
        isAnswered = true;
        nextButton.SetActive(true);
    }

    public void NextQuestion()
    {
        GetRandomQuestion();
    }
}