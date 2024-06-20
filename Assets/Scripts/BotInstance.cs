
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BotInstance : MonoInstance<BotInstance>
{
    public List<BotAnswers> botAnswers = new List<BotAnswers>();
    
    public delegate void BotAnswered(int index);
    public static event BotAnswered OnBotAnswered;
    
    public class BotAnswers
    {
        public answerType answerType;
        public float answerTime;
    }
    public enum answerType
    {
        Correct,
        Incorrect
    }
    protected override void Init()
    {
        for (int i = 0; i < QuizManager.QuestionsCount(); i++)
        {
            botAnswers.Add(new ()
            {
                answerType = Random.Range(0, 2) == 0 ? answerType.Correct : answerType.Incorrect,
                answerTime = Random.Range(1f, 5f)
            });
        }
        StartCoroutine(BotAnswering());
    }
    IEnumerator BotAnswering()
    {
        for (int i = 0; i < botAnswers.Count; i++)
        {
            yield return new WaitForSeconds(botAnswers[i].answerTime);
            OnBotAnswered?.Invoke(i);
        }
    }
    
    public static int CorrectAnswers()
    {
        int count = 0;
        foreach (var botAnswer in Instance.botAnswers)
        {
            if (botAnswer.answerType == answerType.Correct)
            {
                count++;
            }
        }
        return count;
    }
    public static int IncorrectAnswers()
    {
        int count = 0;
        foreach (var botAnswer in Instance.botAnswers)
        {
            if (botAnswer.answerType == answerType.Incorrect)
            {
                count++;
            }
        }
        return count;
    }

    public static float Time()
    {
        float time = 0;
        foreach (var botAnswer in Instance.botAnswers)
        {
            time += botAnswer.answerTime;
        }
        return time;
    }
    
}