using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SortWord : MonoBehaviour
{
    [Header("Answer Settings")]
    public string correctAnswer = "ABC"; // คำตอบที่ถูก
    public Transform answerSlotParent; // Grid ช่องคำตอบ
    public GameObject answerSlotPrefab;

    [Header("Letter Grid")]
    public Transform letterGridParent;
    public GameObject letterButtonPrefab;
    public Button shuffleButton;

    private List<TextMeshProUGUI> answerSlots = new List<TextMeshProUGUI>();
    private List<Button> letterButtons = new List<Button>();

    private string currentAnswer = "";

    void Start()
    {
        SetupAnswerSlots();
        SetupLetterGrid();
        shuffleButton.onClick.AddListener(ShuffleLetters);
    }

    void SetupAnswerSlots()
    {
        foreach (Transform child in answerSlotParent)
            Destroy(child.gameObject);

        answerSlots.Clear();

        foreach (char c in correctAnswer)
        {
            GameObject slot = Instantiate(answerSlotPrefab, answerSlotParent);
            TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
            slotText.text = "";
            answerSlots.Add(slotText);
        }
    }

    void SetupLetterGrid()
    {
        foreach (Transform child in letterGridParent)
            Destroy(child.gameObject);

        letterButtons.Clear();

        List<char> letters = new List<char>(correctAnswer.ToCharArray());

        // ใส่ตัวอักษรหลอก (extra letters)
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

        if (currentAnswer.Length == correctAnswer.Length)
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        if (currentAnswer == correctAnswer)
        {
            Debug.Log("Correct!");
            // TODO: แสดง Scroll ปลดล็อก
        }
        else
        {
            Debug.Log("Wrong!");
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
        SetupLetterGrid();
        ResetAnswer();
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
}
