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
    [SerializeField] private TMP_Text _time;
    
    [SerializeField] private TMP_Text _botWrongAnswers;
    [SerializeField] private TMP_Text _botRightAnswers;
    [SerializeField] private TMP_Text _botTime;
    
    [SerializeField] private Image _resultImage;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _showTime = 0.2f;
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _wrongAnswers.text = QuizManager.WrongAnswers.ToString();
        _rightAnswers.text = QuizManager.CorrectAnswers.ToString();
        _time.text = Time.timeSinceLevelLoad.ToString();
        _botWrongAnswers.text = BotInstance.IncorrectAnswers().ToString();
        _botRightAnswers.text = BotInstance.CorrectAnswers().ToString();
        _botTime.text = BotInstance.Time().ToString();
        if (QuizManager.CorrectAnswers > BotInstance.CorrectAnswers())
        {
            PlayerInstance.points += QuizManager.CorrectAnswers;
            _resultImage.sprite = _goodSprite;
        }
        else if (QuizManager.CorrectAnswers == BotInstance.CorrectAnswers() && Time.timeSinceLevelLoad < BotInstance.Time())
        {
            PlayerInstance.points += QuizManager.CorrectAnswers;
            _resultImage.sprite = _goodSprite;
        }
        else if (QuizManager.CorrectAnswers == BotInstance.CorrectAnswers() && Time.timeSinceLevelLoad >= BotInstance.Time())
        {
            _resultImage.sprite = _neutralSprite;
        }
        else
        {
            _resultImage.sprite = _badSprite;
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