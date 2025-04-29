using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionListManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject questionItemPrefab;
    public QuestionEditor editor;
    public TMP_Text filterText;

    public List<QuestionData> questionList = new List<QuestionData>();

    private string currentFilter = "";

    private void Start()
    {
        RefreshList();
    }

    public void RefreshList()
    {
        // Clear existing items
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Create filtered items
        foreach (var question in questionList)
        {
            if (string.IsNullOrEmpty(currentFilter) ||
                question.questionText.Contains(currentFilter) ||
                question.mode.Contains(currentFilter))
            {
                CreateQuestionItem(question);
            }
        }
    }

    private void CreateQuestionItem(QuestionData question)
    {
        GameObject item = Instantiate(questionItemPrefab, contentPanel);
        item.GetComponentInChildren<TextMeshProUGUI>().text = $"{question.mode}: {question.questionText}";

        item.transform.Find("EditButton").GetComponent<Button>().onClick.AddListener(() => {
            editor.LoadQuestion(question);
        });

        item.transform.Find("DeleteButton").GetComponent<Button>().onClick.AddListener(() => {
            questionList.Remove(question);
            RefreshList();
        });
    }

    public void SetFilter(string filter)
    {
        currentFilter = filter;
        RefreshList();
    }
}