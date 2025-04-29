using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionListManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject questionItemPrefab;
    public QuestionEditor editor;

    public List<QuestionData> questionList = new List<QuestionData>();

    void LoadQuestions()
    {
        foreach (var question in questionList)
        {
            GameObject item = Instantiate(questionItemPrefab, contentPanel);
            item.GetComponentInChildren<Text>().text = question.mode;
            item.transform.Find("EditButton").GetComponent<Button>().onClick.AddListener(() => {
                editor.LoadQuestion(question);
            });
        }
    }

}
