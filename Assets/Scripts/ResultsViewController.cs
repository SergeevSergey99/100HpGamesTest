using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsViewController : MonoBehaviour
{
    [SerializeField] private Sprite _goodSprite;
    [SerializeField] private Sprite _badSprite;
    [SerializeField] private Sprite _neutralSprite;
    [SerializeField] private TMP_Text _wrongAnswers;
    [SerializeField] private TMP_Text _rightAnswers;
    [SerializeField] private Image _resultImage;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _showTime = 0.2f;
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _wrongAnswers.text = QuizManager.WrongAnswers.ToString();
        _rightAnswers.text = QuizManager.CorrectAnswers.ToString();
        if (QuizManager.CorrectAnswers < QuizManager.WrongAnswers)
        {
            _resultImage.sprite = _badSprite;
        }
        else if (QuizManager.WrongAnswers == 0)
        {
            _resultImage.sprite = _goodSprite;
        }
        else
        {
            _resultImage.sprite = _neutralSprite;
        }
        StartCoroutine(Showing());
        
    }
    IEnumerator Showing()
    {
        var time = 0f;
        while (time < _showTime)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = time / _showTime;
            yield return null;
        }
        _canvasGroup.alpha = 1;
    }
    
    
}