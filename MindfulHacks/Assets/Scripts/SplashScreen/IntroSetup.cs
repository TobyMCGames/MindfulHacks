using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSetup : MonoBehaviour
{
    [SerializeField] private QuestionManager questionManager;

    [SerializeField] TextAsset MoodFile;
    [SerializeField] TextAsset ProblemFile;

    // Start is called before the first frame update
    void Start()
    {
        List<string> Moods = CSVHandler.ReadCSV(MoodFile);
        List<string> Problems = CSVHandler.ReadCSV(ProblemFile);
        if (Moods != null)
            questionManager.AddQuestion("How are you feeling today", Moods);
        if (Problems != null)
            questionManager.AddQuestion("Why do you feel this way", Problems);

        questionManager.NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAnswers()
    {
        if (questionManager.SelectedAnswers.Count == 0)
            return;

        switch (questionManager.curQuestion)
        {
            case 1:
                PlayerManager.Instance.data.Problems = questionManager.SelectedAnswers;
                PlayerManager.Instance.SaveProgress();
                break;
        }

        if (!questionManager.AnsweredQuestion())
        {
            PlayerManager.Instance.data.FirstLogin = false;
            NavigationManager.LoadScene("HomeScreen");
        }
    }
}
