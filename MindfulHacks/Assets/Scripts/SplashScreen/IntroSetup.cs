using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSetup : MonoBehaviour
{
    [SerializeField] private QuestionManager questionManager;

    [SerializeField] TextAsset MoodFile;
    [SerializeField] TextAsset ProblemFile;
    [SerializeField] TextAsset UserTypeFile;

    // Start is called before the first frame update
    void Start()
    {
        List<string> Moods = CSVHandler.ReadCSV(MoodFile);
        List<string> Problems = CSVHandler.ReadCSV(ProblemFile);
        List<string> UserTypes = CSVHandler.ReadCSV(UserTypeFile);

        questionManager.AddQuestion("Hey there! What's your name?", null, QuestionManager.QUESTION_TYPES.STRING_INPUT);
        questionManager.AddQuestion("What do you need help with?", UserTypes, QuestionManager.QUESTION_TYPES.SINGLE_MCQ);

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
            case 0:
                PlayerManager.Instance.data.name = questionManager.stringInput.text;
                PlayerManager.Instance.SaveProgress();
                break;
            case 1:
                PlayerManager.Instance.data.UserType = questionManager.SelectedAnswers[0];
                PlayerManager.Instance.SaveProgress();
                break;
            case 3:
                PlayerManager.Instance.data.Problems = questionManager.SelectedAnswers;
                PlayerManager.Instance.SaveProgress();
                break;
        }

        if (!questionManager.AnsweredQuestion())
        {
            PlayerManager.Instance.ReadData();
            PlayerManager.Instance.data.FirstLogin = false;
            NavigationManager.LoadScene("HomeScreen");
        }
    }
}
