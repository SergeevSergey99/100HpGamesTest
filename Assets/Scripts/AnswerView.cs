using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerView : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _answerText;
    [SerializeField] private Button _button;
    int _index;

    Color _defaultColor;
    Color _wrongColor = Color.red;
    Color _rightColor = Color.green;
    
    private void Awake()
    {
        _button.onClick.AddListener(Answer);
        _defaultColor = _button.colors.normalColor;
    }


    public void Init(string answer, int index)
    {
        _index = index;
        _button.image.color = _defaultColor;
        gameObject.SetActive(true);
        _answerText.text = answer;
    }
    
    public void Answer()
    {
        _button.image.color = (QuizManager.isAnswerCorrect(_index))? _rightColor : _wrongColor;
        QuizManager.Answer(_index);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _button.image.color = _defaultColor;
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
