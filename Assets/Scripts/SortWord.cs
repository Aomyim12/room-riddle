using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class SortWord : MonoBehaviour
{
    [Header("Question Settings")]
    public List<QuestionData> questions = new List<QuestionData>();
    private int currentQuestionIndex = 0;

    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI progressText;
    public Transform answerSlotParent;
    public Transform letterGridParent;
    public GameObject answerSlotPrefab;
    public GameObject letterButtonPrefab;
    public Button shuffleButton;
    public GameObject panelToClose;

    private List<TextMeshProUGUI> answerSlots = new List<TextMeshProUGUI>();
    private List<Button> letterButtons = new List<Button>();

    private string currentAnswer = "";

    void Start()
    {
        LoadQuestion();
        shuffleButton.onClick.AddListener(ShuffleLetters);
    }

    void LoadQuestion()
    {
        // เคลียร์ UI เดิม
        foreach (Transform child in answerSlotParent)
            Destroy(child.gameObject);
        foreach (Transform child in letterGridParent)
            Destroy(child.gameObject);

        answerSlots.Clear();
        letterButtons.Clear();
        currentAnswer = "";

        // ดึงคำถามปัจจุบัน
        QuestionData q = questions[currentQuestionIndex];
        questionText.text = q.questionText;
        progressText.text = $"{currentQuestionIndex + 1}/{questions.Count}";

        // สร้างช่องคำตอบ
        foreach (char c in q.correctAnswer)
        {
            GameObject slot = Instantiate(answerSlotPrefab, answerSlotParent);
            TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
            slotText.text = "";
            answerSlots.Add(slotText);
        }

        // สร้างปุ่มตัวอักษร
        List<char> letters = new List<char>(q.correctAnswer.ToCharArray());

        // เพิ่มตัวหลอกให้ครบ 9 ตัว
        while (letters.Count < 9)
        {
            char randomChar = (char)('A' + Random.Range(0, 26));
            letters.Add(randomChar);
        }

        ShuffleList(letters);

        foreach (char c in letters)
        {
            GameObject btnObj = Instantiate(letterButtonPrefab, letterGridParent);
            Button btn = btnObj.GetComponent<Button>();
            TextMeshProUGUI txt = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = c.ToString();
            btn.onClick.AddListener(() => OnLetterClicked(txt.text, btn));
            letterButtons.Add(btn);
        }
    }

    void OnLetterClicked(string letter, Button btn)
    {
        for (int i = 0; i < answerSlots.Count; i++)
        {
            if (answerSlots[i].text == "")
            {
                answerSlots[i].text = letter;
                currentAnswer += letter;
                btn.interactable = false;
                break;
            }
        }

        if (currentAnswer.Length == questions[currentQuestionIndex].correctAnswer.Length)
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        string correct = questions[currentQuestionIndex].correctAnswer;

        if (currentAnswer == correct)
        {
            Debug.Log("✅ Correct!");

            // ถ้ายังมีข้อถัดไป
            if (currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
                LoadQuestion();
            }
            else
            {
                Debug.Log("🎉 All questions completed!");
                progressText.text = "Completed!";
                questionText.text = "You’ve finished all words!";

                Invoke("ReturnMenu", 2f);
            }
        }
        else
        {
            Debug.Log("❌ Wrong!");
            ResetAnswer();
        }
    }

    void ResetAnswer()
    {
        currentAnswer = "";
        foreach (var slot in answerSlots)
            slot.text = "";

        foreach (var btn in letterButtons)
            btn.interactable = true;
    }

    void ShuffleLetters()
    {
        LoadQuestion();
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    void ReturnMenu()
    {
        if (panelToClose != null)
            panelToClose.SetActive(false);
    }
}
