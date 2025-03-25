using UnityEngine;
using TMPro;
using System;

public class TimerBehaviour : MonoBehaviour
{
    public int StartValue = 99;
    [HideInInspector] public int displayTime;
    private float startTime;
    private TextMeshProUGUI timer;

    void Start()
    {
        startTime = Time.time;
        timer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float timeSinceStart = Time.time - startTime;
        displayTime = StartValue - (int) Math.Ceiling(timeSinceStart);
        displayTime = Math.Max(0, displayTime);
        timer.text = displayTime.ToString();
    }
}
