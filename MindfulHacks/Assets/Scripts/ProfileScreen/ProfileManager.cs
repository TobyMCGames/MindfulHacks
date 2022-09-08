using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text Email;
    [SerializeField] private Text UserType;
    [SerializeField] private Text HelpWith;

    [SerializeField] private TextAsset MoodFile;
    [SerializeField] private TextAsset ProblemFile;

    [SerializeField] private GameObject QuestionScreen;

    [SerializeField] private QuestionManager questionManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateProfile();

        List<string> Moods = CSVHandler.ReadCSV(MoodFile);
        List<string> Problems = CSVHandler.ReadCSV(ProblemFile);

        if (Moods != null)
            questionManager.AddQuestion("How are you feeling today?", Moods, QuestionManager.QUESTION_TYPES.MCQ);
        if (Problems != null)
            questionManager.AddQuestion("Why do you feel this way?", Problems, QuestionManager.QUESTION_TYPES.MCQ);

        questionManager.NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void UpdateProfile()
    {
        Name.text = "Name: " + PlayerManager.Instance.data.name;
        Email.text = "Email: " + PlayerManager.Instance.data.email;
        UserType.text = "User Type: " + PlayerManager.Instance.data.UserType;
        HelpWith.text = "Need Help with : ";
        for (int i = 0; i < PlayerManager.Instance.data.Problems.Count; i++)
        {
            if (i > 0)
                HelpWith.text += ", ";
            HelpWith.text += PlayerManager.Instance.data.Problems[i];
        }

        Debug.Log("You have clicked the button!");
    }

    public void GetAnswers()
    {
        switch (questionManager.QuestionTypeOrder[questionManager.curQuestion])
        {
            case QuestionManager.QUESTION_TYPES.MCQ:
                if (questionManager.SelectedAnswers.Count == 0)
                {
                    return;
                }
                break;
            case QuestionManager.QUESTION_TYPES.STRING_INPUT:
                if (questionManager.stringInput.text == "")
                {
                    return;
                }
                break;
        }

        switch (questionManager.curQuestion)
        {
            case 1:
                PlayerManager.Instance.data.Problems = questionManager.SelectedAnswers;
                PlayerManager.Instance.SaveProgress();
                break;
        }

        if (!questionManager.AnsweredQuestion())
        {
            PlayerManager.Instance.ReadData();
            QuestionScreen.SetActive(false);
            NavigationManager.Instance.ShowNav(true);
            UpdateProfile();
        }
    }

    public void ShowNav(bool _show)
    {
        NavigationManager.Instance.ShowNav(_show);
    }
}
