using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastPointLife : MonoBehaviour
{
    public float maxH = 500;
    public float curH = 500;

    public GameObject PointHp;
    public GameObject HpText;

    public GameObject Door;

    GameObject directer;

    void Start()
    {
        PointHp = GameObject.Find("lastPointHp");
        HpText = GameObject.Find("HpText");
        directer = GameObject.Find("GameManager");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            if (curH <= 0)
            {
                gameObject.layer = 12;
                curH = 0;
                End();
            }
            curH -= GameObject.Find("lowerarm_r").GetComponent<BornenemyWeapon>().damage;
            PointHp.GetComponent<Image>().fillAmount = curH / maxH;
            HpText.GetComponent<Text>().text = curH.ToString();
            
        }
        else if (other.CompareTag("BossWeapon"))
        {
            if (curH <= 0)
            {
                gameObject.layer = 12;
                curH = 0;
                End();
            }
            curH -= GameObject.Find("sword").GetComponent<BornenemyWeapon>().damage;
            PointHp.GetComponent<Image>().fillAmount = curH / maxH;
            HpText.GetComponent<Text>().text = curH.ToString();
        }
        else if (other.CompareTag("zombieWeapon"))
        {
            if (curH <= 0)
            {
                gameObject.layer = 12;
                curH = 0;
                End();
            }
            curH -= GameObject.Find("lower_arm.R").GetComponent<BornenemyWeapon>().damage;
            PointHp.GetComponent<Image>().fillAmount = curH / maxH;
            HpText.GetComponent<Text>().text = curH.ToString();
        }
    }
    public void End()
    {
        directer.GetComponent<gameDirecter>().GameEnd();
    }
}
