using UnityEngine;
using TMPro;
using System;

public class TimerBehaviour : MonoBehaviour
{
    public int StartValue = 99;
    [HideInInspector] public float displayTime;
    private TextMeshProUGUI timer;

    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        ResetTime();
    }

    void Update()
    {
        displayTime -= Time.deltaTime;
        
        int timeOnTimer = Math.Max(0, (int) Math.Ceiling(displayTime));
        timer.text = timeOnTimer.ToString();
    }

    public void ResetTime()
    {
        displayTime = StartValue;
    }
}
