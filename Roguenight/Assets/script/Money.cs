using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int money = 200;
    int curmoney = 200;
    public GameObject MoneyText;

    void Start()
    {
        MoneyText = GameObject.Find("text_money");
    }

    void Update()
    {
        if (money != curmoney)
        {
            curmoney = money;
            MoneyText.GetComponent<Text>().text = "money: " + curmoney.ToString();
        }
    }
}
