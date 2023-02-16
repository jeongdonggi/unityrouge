using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject MonsterPrefab;
    public GameObject MonsterPrefab2;
    float span = 2f;
    float delta = 0;
    public int count = 0; //생성 갯수 제한

    public int monnum = 1;
    GameObject go;
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span & count < 30) // 60초까지 초당 1마리
        {
            this.delta = 0;
            if (monnum == 1)
            {
                go = Instantiate(MonsterPrefab) as GameObject;
            }
            else
            {
                Debug.Log("다음 시작");
                go = Instantiate(MonsterPrefab2) as GameObject;
            }
            int px = Random.Range(-9, 9);
            go.transform.position = new Vector3(px, 0, 80);
            count += 1; //생성 갯수 정해주기
        }
    }
}
