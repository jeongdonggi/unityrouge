using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdate : MonoBehaviour
{
    GameObject timerText;
    float time = 60.0f;
    void Start()
    {
        this.timerText = GameObject.Find("text_time");
    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.time.ToString("F0")+"s";
        if (time <= 0)
        {
            this.timerText.GetComponent<Text>().text = "Boss";
        }
    }
}
