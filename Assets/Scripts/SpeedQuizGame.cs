using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SpeedQuizGameTMP : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public List<Question> questions = new List<Question>();
    private Question currentQuestion;

    // �� TMP ᷹ Text ����
    public TMP_Text questionText;
    public TMP_Text scoreText;
    public TMP_Text timerText;   
    public Button[] answerButtons;
    public TMP_Text[] answerTexts; // Text 㹻����ӵͺ
    

    private int score = 0;
    private float timeRemaining = 30f;
    private bool isGameActive = false;
    private int currentQuestionIndex = 0;

    void Start()
    {
        

        // ��駤�һ����ӵͺ
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i;
            answerButtons[i].onClick.AddListener(() => AnswerButtonClicked(buttonIndex));
        }

        StartGame();
    }

    void Update()
    {
        if (isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = $"Time: {Mathf.RoundToInt(timeRemaining)} S";

            if (timeRemaining <= 0f)
            {
                isGameActive = false;
                SceneManager.LoadScene("ChooseRoom"); // ����¹���� Scene ���ç�Ѻ��������� Build Settings
            }
        }
    }

    public void StartGame()
    {
        score = 0;
        timeRemaining = 30f;
        isGameActive = true;
        scoreText.text = $"Score: {score}";

        GetRandomQuestion();
    }

    void GetRandomQuestion()
    {
        if (questions.Count > 0)
        {
            currentQuestionIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[currentQuestionIndex];

            questionText.text = currentQuestion.questionText;

            ShuffleAnswers();

            for (int i = 0; i < answerTexts.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerTexts[i].text = currentQuestion.answers[i];
                    answerButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void ShuffleAnswers()
    {
        for (int i = 0; i < currentQuestion.answers.Length; i++)
        {
            string temp = currentQuestion.answers[i];
            int randomIndex = Random.Range(i, currentQuestion.answers.Length);
            currentQuestion.answers[i] = currentQuestion.answers[randomIndex];
            currentQuestion.answers[randomIndex] = temp;

            if (i == currentQuestion.correctAnswerIndex)
            {
                currentQuestion.correctAnswerIndex = randomIndex;
            }
            else if (randomIndex == currentQuestion.correctAnswerIndex)
            {
                currentQuestion.correctAnswerIndex = i;
            }
        }
    }

    void AnswerButtonClicked(int buttonIndex)
    {
        if (!isGameActive) return;

        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            score += 10;
            scoreText.text = $"Score: {score}";
            timeRemaining += 3f;

            // ����¹�բ�ͤ���᷹�������¹�ջ���
            answerTexts[buttonIndex].color = Color.green;
        }
        else
        {
            score = Mathf.Max(0, score - 5);
            scoreText.text = $"Score: {score}";
            timeRemaining = Mathf.Max(1f, timeRemaining - 2f);

            answerTexts[buttonIndex].color = Color.red;
            answerTexts[currentQuestion.correctAnswerIndex].color = Color.green;
        }

        questions.RemoveAt(currentQuestionIndex);
        StartCoroutine(NextQuestionAfterDelay(1.5f));
    }

    IEnumerator NextQuestionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // �����բ�ͤ���
        foreach (TMP_Text answerText in answerTexts)
        {
            answerText.color = Color.black; // ������������س��
        }

        if (questions.Count > 0)
        {
            GetRandomQuestion();
        }
        else
        {
            // ����ͤӶ�����
            isGameActive = false;
            SceneManager.LoadScene("ChooseRoom");
        }
    }



}