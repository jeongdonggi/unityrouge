using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameDirecter : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject endPanel;
    public GameObject stopPanel;
    public GameObject clearPanel;

    public GameObject gameCam;
    public GameObject endCam;

    public GameObject pasue;

    public Player player;
    public MonsterGenerator monster;
    public TimeUpdate time;

    public GameObject boss2;
    public GameObject time2;

    float t = 10.0f;
    float a = 0;
    float b = 0;
    int c = 0;

    public int kill_point = 0;
    public int stage = 1;

    public void GameEnd()
    {
        gameCam.SetActive(false);
        endCam.SetActive(true);

        gamePanel.SetActive(false);
        stopPanel.SetActive(false);
        endPanel.SetActive(true);
        clearPanel.SetActive(false);

        player.gameObject.SetActive(false);
        monster.gameObject.SetActive(false);
        time.gameObject.SetActive(false);
        pasue.SetActive(false);
    }
    public void GameClaer()
    {
        gameCam.SetActive(false);
        endCam.SetActive(true);

        gamePanel.SetActive(false);
        stopPanel.SetActive(false);
        endPanel.SetActive(false);
        clearPanel.SetActive(true);

        player.gameObject.SetActive(false);
        monster.gameObject.SetActive(false);
        time.gameObject.SetActive(false);
        pasue.SetActive(false);
    }
    void Update()
    {
        this.a = Time.deltaTime;
        if (kill_point == 31)
        {
            Clear();
            if (stage == 1)
            {
                nextstage();
            }
            else
            {
                GameClaer();
            }
        }
        
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void quit()
    {
        Application.Quit();
    }
    public void Up()
    {
        kill_point += 1;
    }
    public void Pause()
    {
        Time.timeScale = 0;

        stopPanel.SetActive(true);
    }
    public void restart()
    {
        Time.timeScale = 1;
        stopPanel.SetActive(false);
    }
    public void Clear()
    {
        Debug.Log("클리어");
        monster.gameObject.SetActive(false);
        kill_point = 0; //다시 0으로 초기화
    }
    public void nextstage()
    {
        stage += 1;
        Debug.Log("다음으로");
        monster.monnum = 2;
        monster.count = 0;
        monster.gameObject.SetActive(true);
        player.idle();
        GameObject.Find("stagenumber").GetComponent<Text>().text = "Stage 2";
        boss2.SetActive(true);
        time2.SetActive(true);
        GameObject.Find("SkeletonWarrior2").GetComponent<boss>().bosnum = 2;
    }
}
