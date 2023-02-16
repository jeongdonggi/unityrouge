using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class readyStart : MonoBehaviour
{
    public void go()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void quit()
    {
        Application.Quit();
    }
}
