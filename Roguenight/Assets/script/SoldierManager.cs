using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierManager : MonoBehaviour
{
    public GameObject Soldier_archer; // 아쳐 솔져 오브젝트 받아주기
    public GameObject Soldier_mage; // 메이지 솔져 오브젝트 받아주기
    public GameObject text_archer;
    public GameObject text_mage;

    int count_archer = 5;
    int count_mage = 5;
    float position_archer = -8;
    float position_mage = 8;
    int archer_money = 100;
    int mage_money = 100;
    
    void Start()
    {
        text_archer = GameObject.Find("text_archer");
        text_mage = GameObject.Find("text_mage");
    }

    public void BuildToArcher()
    {
        if (count_archer > 0 & GameObject.Find("Money").GetComponent<Money>().money >= archer_money)
        {
            Instantiate(Soldier_archer, transform.position = new Vector3(position_archer, 2.4f, 14.9f), Quaternion.identity);
            position_archer = position_archer -2f;
            count_archer = count_archer - 1;
            GameObject.Find("Money").GetComponent<Money>().money -= archer_money; // 돈 쓰고 업그레이드 함
            archer_money += 100;
            text_archer.GetComponent<Text>().text = "archer"+ '\n' + "+1" + '\n' + archer_money + "m";
        }
        else if(count_archer == 0)
        {
            text_archer.GetComponent<Text>().text = "Max";
        }
        

    }
    public void BuildToMage()
    {
        if(count_mage > 0 & GameObject.Find("Money").GetComponent<Money>().money >= mage_money)
        {
            Instantiate(Soldier_mage, transform.position = new Vector3(position_mage, 2.4f, 14.9f), Quaternion.identity);
            position_mage = position_mage + 2f;
            GameObject.Find("Money").GetComponent<Money>().money -= mage_money; // 돈 쓰고 업그레이드 함
            count_mage = count_mage - 1;
            mage_money += 100;
            text_mage.GetComponent<Text>().text = "mage" + '\n' + "+1" + '\n' + mage_money + "m";
        }
        else if (count_mage == 0)
        {
            text_mage.GetComponent<Text>().text = "Max";
        }
    }

}
