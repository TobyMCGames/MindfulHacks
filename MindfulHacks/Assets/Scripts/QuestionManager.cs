using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public enum QUESTION_TYPES
    {
        MCQ,
        SINGLE_MCQ,
        STRING_INPUT
    }

    [SerializeField] private Text QuestionText;
    [SerializeField] private RectTransform QuestionContent;
    [SerializeField] private Button AnswerPrefab;

    [SerializeField] private GameObject MCQ_Object;
    [SerializeField] private GameObject StringInput_Object;

    public InputField stringInput;

    public MyDictionary<string, List<string>> Questions;
    public List<QUESTION_TYPES> QuestionTypeOrder = new List<QUESTION_TYPES>();
    public List<string> SelectedAnswers = new List<string>();

    public int curQuestion = -1;

    void Awake()
    {
        curQuestion = -1;
    }

    public void AddQuestion(string _question, List<string> _Asnwers, QUESTION_TYPES _type)
    {
        Questions.Add(_question, _Asnwers);
        QuestionTypeOrder.Add(_type);
    }

    public bool NextQuestion()
    {
        if (curQuestion >= Questions.Count - 1)
            return false;

        curQuestion++;

        // Set Appropriate Question
        switch (QuestionTypeOrder[curQuestion])
        {
            case QUESTION_TYPES.SINGLE_MCQ:
                MCQ_Object.SetActive(true);
                break;
            case QUESTION_TYPES.MCQ:
                MCQ_Object.SetActive(true);
                break;
            case QUESTION_TYPES.STRING_INPUT:
                StringInput_Object.SetActive(true);
                break;
        }

        // Getting Question
        QuestionText.text = Questions.ElementAt(curQuestion).a;

        // For MCQ generation
        if (QuestionTypeOrder[curQuestion] != QUESTION_TYPES.MCQ && QuestionTypeOrder[curQuestion] != QUESTION_TYPES.SINGLE_MCQ)
            return true;

        // Getting Answers
        List<string> answers = Questions.ElementAt(curQuestion).b;

        // Organising
        Vector2 ContentSize = new Vector2(Screen.width / QuestionContent.lossyScale.x - 15f, 0);
        Vector2 AnswerSize = AnswerPrefab.GetComponent<RectTransform>().sizeDelta;
        float Padding = (ContentSize.x % AnswerSize.x) / ((int)(ContentSize.x / AnswerSize.x) + 1);

        Vector2 offset = new Vector2();
        for (int i = 0; i < answers.Count; i++)
        {
            Button answerBtn = Instantiate<Button>(AnswerPrefab, QuestionContent);
            if (QuestionTypeOrder[curQuestion] == QUESTION_TYPES.MCQ)
                answerBtn.onClick.AddListener(delegate { SelectMultiAnswer(answerBtn.transform.GetChild(0).GetComponent<Text>().text); });
            else if (QuestionTypeOrder[curQuestion] == QUESTION_TYPES.SINGLE_MCQ)
                answerBtn.onClick.AddListener(delegate { SelectOneAnswer(answerBtn.transform.GetChild(0).GetComponent<Text>().text); });

            // Assign Positions
            RectTransform answerBox = answerBtn.GetComponent<RectTransform>();

            offset.x += Padding;
            answerBox.anchoredPosition = offset;

            // Changing offset
            offset.x += AnswerSize.x;
            if (offset.x + AnswerSize.x > ContentSize.x)
            {
                offset.x = 0;
                offset.y -= Padding + AnswerSize.y;
                if (Mathf.Abs(offset.y) + AnswerSize.y > ContentSize.y)
                {
                    ContentSize.y = Mathf.Abs(offset.y) + AnswerSize.y;
                    QuestionContent.offsetMin = new Vector2(0, -ContentSize.y);

                }
            }

            // Assigning Answers
            Text answerText = answerBtn.transform.GetChild(0).GetComponent<Text>();
            answerText.text = answers[i];


        }

        return true;
    }

    public void ClearAnswers()
    {
        for (int i = 0; i < QuestionContent.childCount; i++)
        {
            Destroy(QuestionContent.GetChild(i).gameObject);
        }

        SelectedAnswers.Clear();
        stringInput.text = "";

        MCQ_Object.SetActive(false);
        StringInput_Object.SetActive(false);
    }

    public bool AnsweredQuestion()
    {
        ClearAnswers();
        return NextQuestion();
    }

    public void SelectMultiAnswer(string _answer)
    {
        for (int i = 0; i < SelectedAnswers.Count; i++)
        {
            if (SelectedAnswers[i] == _answer)
            {
                SelectedAnswers.RemoveAt(i);
                return;
            }
        }

        SelectedAnswers.Add(_answer);
        return;
    }

    public void SelectOneAnswer(string _answer)
    {
        if (SelectedAnswers.Count >= 1)
            SelectedAnswers.Clear();

        SelectedAnswers.Add(_answer);
    }
}
