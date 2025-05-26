using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HintQuizGame : MonoBehaviour
{
    [System.Serializable]
    public class QuizItem
    {
        public string answer; // �ӵͺ���١��ͧ
        public string[] hints; // �� 3 ���
    }

    public List<QuizItem> quizItems = new List<QuizItem>();
    private QuizItem currentQuiz;

    public TMP_Text[] hintTexts; // Text ����Ѻ�� 3 ���
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
        // ��駤�һ���
        submitButton.onClick.AddListener(CheckAnswer);
        nextButton.SetActive(false);

        // ����������¤Ӷ���á
        GetRandomQuestion();
    }

    void GetRandomQuestion()
    {
        if (quizItems.Count > 0)
        {
            // �����Ӷ��
            int randomIndex = Random.Range(0, quizItems.Count);
            currentQuiz = quizItems[randomIndex];

            // ź�Ӷ������������͡�ҡ��ʵ�
            quizItems.RemoveAt(randomIndex);

            // ����ʶҹ�
            currentHintIndex = 0;
            isAnswered = false;
            answerInput.text = "";
            resultText.text = "";
            nextButton.SetActive(false);

            // �ʴ����á
            ShowHint(0);
        }
        else
        {
            // ��ҤӶ�����
            resultText.text = "����! ��ṹ���: " + score;
            submitButton.gameObject.SetActive(false);
            answerInput.gameObject.SetActive(false);
        }
    }

    void ShowHint(int index)
    {
        if (index < currentQuiz.hints.Length)
        {
            // �ʴ������ӴѺ
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
            // �ͺ�١
            int points = (3 - currentHintIndex) * 10; // ���ṹ�ҡ��鹶�ҵͺ��������¢��
            score += points;

            resultText.text = "�١��ͧ! +" + points + " ��ṹ";
            resultText.color = Color.green;
        }
        else
        {
            // �ͺ�Դ
            if (currentHintIndex < 2)
            {
                // �ʴ�����������ѧ��
                ShowHint(currentHintIndex + 1);
                resultText.text = "���١��ͧ �ͧ��������!";
                resultText.color = Color.red;
                return;
            }
            else
            {
                // �ʴ��ӵͺ�������ú����
                resultText.text = "����㨴���! �ӵͺ���: " + currentQuiz.answer;
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