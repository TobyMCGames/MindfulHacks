using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private Text QuestionText;
    [SerializeField] private RectTransform QuestionContent;
    [SerializeField] private Button AnswerPrefab;

    public MyDictionary<string, List<string>> Questions;
    public List<string> SelectedAnswers = new List<string>();

    public int curQuestion = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddQuestion(string _question, List<string> _Asnwers)
    {
        Questions.Add(_question, _Asnwers);
    }

    public bool NextQuestion()
    {
        if (curQuestion >= Questions.Count)
            return false;

        // Getting Question
        QuestionText.text = Questions.ElementAt(curQuestion).a;
        List<string> answers = Questions.ElementAt(curQuestion).b;

        // Organising
        Vector2 ContentSize = new Vector2(Screen.width / QuestionContent.lossyScale.x - 15f, 0);
        Vector2 AnswerSize = AnswerPrefab.GetComponent<RectTransform>().sizeDelta;
        float Padding = (ContentSize.x % AnswerSize.x) / ((int)(ContentSize.x / AnswerSize.x) + 1);

        Vector2 offset = new Vector2();
        for (int i = 0; i < answers.Count; i++)
        {
            Button answerBtn = Instantiate<Button>(AnswerPrefab, QuestionContent);
            answerBtn.onClick.AddListener(delegate { SelectAnswer(answerBtn.transform.GetChild(0).GetComponent<Text>().text); });

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

        curQuestion++;

        return true;
    }

    public void ClearAnswers()
    {
        for (int i = 0; i < QuestionContent.childCount; i++)
        {
            Destroy(QuestionContent.GetChild(i).gameObject);
        }

        SelectedAnswers.Clear();
    }

    public bool AnsweredQuestion()
    {
        ClearAnswers();
        return NextQuestion();
    }

    public void SelectAnswer(string _answer)
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
}
