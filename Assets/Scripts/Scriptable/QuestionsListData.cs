using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "ScriptableObjects/QuestionsData", order = 0)]
public class QuestionsListData : ScriptableObject
{
    [SerializeField] private List<QuestionData> _questions;

    public IReadOnlyList<QuestionData> QuestionDatas => _questions;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < _questions.Count; i++)
        {
            _questions[i]?.Validate();
        }
    }
#endif
}

[Serializable]
public class QuestionData
{
    [SerializeField, TextArea(3, 10)] private string _question;
    [SerializeField, Range(0, 3)] private int _correctAnswerIndex;
    [SerializeField] List<AnswerData> _answers = new();

    public string Question => _question;
    public int CorrectAnswerIndex => _correctAnswerIndex;
    public IReadOnlyList<AnswerData> AnswersDatas => _answers;
    
#if UNITY_EDITOR
    public void Validate()
    {
        _correctAnswerIndex = Mathf.Clamp(_correctAnswerIndex, 0, _answers.Count - 1);
    }
#endif
}

[Serializable]
public class AnswerData
{
    [SerializeField] private string _answer;
    public string Answer => _answer;
}
