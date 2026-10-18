using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    float gamePlaytime = 0;

    // Start is called before the first frame update
    void Start()
    {
        gamePlaytime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        gamePlaytime += Time.deltaTime;
        timeText.text = "Time : "+ gamePlaytime.ToString("F1");
    }
}