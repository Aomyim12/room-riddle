using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionEditor : MonoBehaviour
{
    public TMP_Dropdown modeDropdown;
    public GameObject QuickResponsePanel;
    public GameObject MatchPanel;
    public GameObject AnsPanel;

    public TMP_InputField inputQuestion;
    public Transform optionsParent;
    public GameObject optionPrefab;

    private QuestionData currentEditing = null;




    void Start()
    {
        modeDropdown.onValueChanged.AddListener(OnModeChanged);
        OnModeChanged(modeDropdown.value); // เรียกตอนเริ่ม
    }

    void OnModeChanged(int index)
    {
        // ปิดทุก panel ก่อน
        QuickResponsePanel.SetActive(false);
        MatchPanel.SetActive(false);
        AnsPanel.SetActive(false);

        // เปิด panel ที่ต้องการ
        switch (index)
        {
            case 0: // สร้างรหัส
                QuickResponsePanel.SetActive(true);
                break;
            case 1: // ตอบเร็ว
                MatchPanel.SetActive(true);
                break;
            case 2: // จับคู่
                AnsPanel.SetActive(true);
                break;
        }
    }
    public void LoadQuestion(QuestionData q)
    {
        currentEditing = q;
        inputQuestion.text = q.questionText;

        int modeIndex = modeDropdown.options.FindIndex(o => o.text == q.mode);
        modeDropdown.value = modeIndex >= 0 ? modeIndex : 0;

        ClearOptions();

        for (int i = 0; i < q.options.Count; i++)
        {
            GameObject opt = Instantiate(optionPrefab, optionsParent);
            opt.GetComponentInChildren<TMP_InputField>().text = q.options[i];
            opt.GetComponentInChildren<Toggle>().isOn = (i == q.correctOptionIndex);
        }
    }


    public void SaveQuestion()
    {
        if (currentEditing == null)
        {
            currentEditing = new QuestionData();
            FindObjectOfType<QuestionManager>().AddQuestion(currentEditing);
        }

        currentEditing.questionText = inputQuestion.text;
        currentEditing.mode = modeDropdown.options[modeDropdown.value].text;
        currentEditing.options = new List<string>();

        int correct = -1;
        for (int i = 0; i < optionsParent.childCount; i++)
        {
            var child = optionsParent.GetChild(i);
            string opt = child.GetComponentInChildren<TMP_InputField>().text;
            bool isCorrect = child.GetComponentInChildren<Toggle>().isOn;
            currentEditing.options.Add(opt);
            if (isCorrect) correct = i;
        }

        currentEditing.correctOptionIndex = correct;
    }

    void ClearOptions()
    {
        foreach (Transform c in optionsParent)
            Destroy(c.gameObject);
    }

    public void AddOption()
    {
        Instantiate(optionPrefab, optionsParent);
    }

}
