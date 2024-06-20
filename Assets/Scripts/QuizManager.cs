
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoInstance<QuizManager>
{
    [SerializeField] private List<QuestionsListData> _questionsListDatas;
    [SerializeField] private QuestionViewController _questionViewController;
    [SerializeField] private ResultsViewController _resultsViewController;
    private int _currentQuizIndex;
    private int _currentQuestionIndex;
    
    private int _correctAnswers;
    private int _wrongAnswers;
    
    public static int CorrectAnswers => Instance._correctAnswers;
    public static int WrongAnswers => Instance._wrongAnswers;
    QuestionData CurrentQuestion => _questionsListDatas[_currentQuizIndex].QuestionDatas[_currentQuestionIndex];

    public static int QuestionsCount()
    {
        int count = 0;
        foreach (var questionsListData in Instance._questionsListDatas)
        {
            count += questionsListData.QuestionDatas.Count;
        }
        return count;
    }

    void Start()
    {
        _currentQuizIndex = 0;
        _currentQuestionIndex = 0;
        _questionViewController.Init(CurrentQuestion);
    }

    public static bool isAnswerCorrect(int answerIndex)
    {
        return answerIndex == Instance.CurrentQuestion.CorrectAnswerIndex;
    }
    public static void Answer(int answerIndex)
    {
        if (isAnswerCorrect(answerIndex))
        {
            Instance._correctAnswers++;
        }
        else
        {
            Instance._wrongAnswers++;
        }
        Instance.NextQuestion();
    }

    private void NextQuestion()
    {
        _currentQuestionIndex++;
        if (_currentQuestionIndex >= _questionsListDatas[_currentQuizIndex].QuestionDatas.Count)
        {
            _currentQuizIndex++;
            _currentQuestionIndex = 0;
            if (_currentQuizIndex >= _questionsListDatas.Count)
            {
                _questionViewController.Hide();
                _resultsViewController.Show();
                return;
            }
        }
        _questionViewController.InitWithAnimation(CurrentQuestion);
    }
}