using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;
    private int minute;
    // Start is called before the first frame update
    void Start()
    {
        UpdateClock();
    }
    private void Update()
    {
        if(minute < System.DateTime.UtcNow.ToLocalTime().Minute)
        {
            UpdateClock();
        }
    }

    void UpdateClock()
    {
        minute = System.DateTime.UtcNow.ToLocalTime().Minute;
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm\ndd-MM-yyyy");
        clockText.text = time;
    }
}
