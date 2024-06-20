using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _pointsText;
    private void Awake()
    {
        PlayerInstance.OnPointsChanged += SetText;
        SetText(PlayerInstance.points);
        
    }
    void SetText(int points)
    {
        _pointsText.text = points.ToString();
    }
    private void OnDestroy()
    {
        PlayerInstance.OnPointsChanged -= SetText;
    }
    
}
