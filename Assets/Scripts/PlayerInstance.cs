using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoInstance<PlayerInstance>
{
    public int _points = 0;
    public static int points
    {
        get => Instance._points;
        set
        {
            Instance._points = value;
            OnPointsChanged?.Invoke(value);
        }
    }
    
    #region events

    public delegate void PointsChanged(int points);
    public static event PointsChanged OnPointsChanged;

    #endregion
    
    #region SingletonPart
   
    private const string pointsKey = "points";
    protected override void Init()
    {
        base.Init();
        _points = PlayerPrefs.GetInt(pointsKey, 0); 
    }
    protected override void DeInit()
    {
        base.DeInit();
        PlayerPrefs.SetInt(pointsKey, _points);
    }

    #endregion

}
