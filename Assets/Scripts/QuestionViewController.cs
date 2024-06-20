using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private List<AnswerView> _answersViews;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] float _animationTime = 0.5f;
    
    public void Init(QuestionData questionData)
    {
        _questionText.text = questionData.Question;
        for (int i = 0; i < questionData.AnswersDatas.Count; i++)
        {
            _answersViews[i].Init(questionData.AnswersDatas[i].Answer, i);
        }

        for (int i = questionData.AnswersDatas.Count; i < _answersViews.Count; i++)
        {
            _answersViews[i].Disable();
        }
    }
    
    Coroutine _coroutine;
    public void InitWithAnimation(QuestionData questionData)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(InitingWithAnimation(questionData));
    }

    public void Hide()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(Hiding());
    }
    IEnumerator Hiding()
    {
        _canvasGroup.interactable = false;
        yield return new WaitForSeconds(_animationTime);
        
        float time = 0;
        while (time < _animationTime / 2)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = 1 - time / _animationTime;
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, Vector3.back*180, time / (_animationTime / 2)));
            yield return null;
        }
        
        _canvasGroup.alpha = 0;
    }
    IEnumerator InitingWithAnimation(QuestionData questionData)
    {
        _canvasGroup.interactable = false;
        yield return new WaitForSeconds(_animationTime);
        
        float time = 0;
        while (time < _animationTime / 2)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = 1 - time / _animationTime;
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, Vector3.back*180, time / (_animationTime / 2)));
            yield return null;
        }
        
        _canvasGroup.alpha = 0;
        Init(questionData);
        
        while (time < _animationTime)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = time / _animationTime;
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.forward*180, Vector3.zero, (time - _animationTime / 2) / (_animationTime / 2)));
            yield return null;
        }
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
    }
}
