using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : MonoBehaviour
{
    public int number;
    public float Range; //공격 범위
    int Archer_Damage = 20;
    int Mage_Damage = 50;
    public GameObject Soldier; // 솔져 오브젝트 받아주기
    public GameObject Splash; //스플래쉬

    GameObject Target; // 몬스터
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Soldier.transform.position, Range); //Range
    }
    void UpdateTarget()
    {
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Enemy"); //몬스터는 일단 다 리스트에 넣기
        float shortestDistance = Mathf.Infinity; //가장 짧은 거리( 최댓값으로 초기화)
        GameObject nearestMonster = null; // 가장 가까운 몬스터
        //foreach문 : 배열을 차례로 접근하여 변수에 담아준다.
        foreach (GameObject Monster in Monsters)
        { //임의의 Monster 오브젝트에 몬스터 배열을 넣어줌
            float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position); // 타워와 몬스터 사이의 거리 구하기
            if (DistanceToMonsters < shortestDistance)
            {
                shortestDistance = DistanceToMonsters; //현재 작은 거리 보다 더 작은 거리일 때를 계속해서 구하기 위해
                nearestMonster = Monster; //이떄의 몬스터를 넣어준다.,
            }
        }
        if (nearestMonster != null && shortestDistance <= Range) // 몬스터가 있고 공격 범위에 들어오게 된다면
        {
            Target = nearestMonster; // 타겟은 가까운 몬스터
            Attack();
        }
        else
        {
            Idle();
            Target = null; // 이름이 없거나 범위에 없으면 멈춰있으세요.
        }
    }

    void Attack() //공격
    {
        anim.SetBool("isAttack", true);
        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        if (number == 1)
        {
            transform.rotation = Quaternion.Euler(0f, rotation.y + 30f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }
    void Idle() // 멈춤 짜야됨
    {
        anim.SetBool("isAttack", false);
        if (number == 1) //아처
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void ArcherDamage() //일단 대기
    {
        GameObject splash = Instantiate(Splash, Target.transform.position, Quaternion.identity);
        splash.GetComponent<Splash>().Damage = Archer_Damage;
        Destroy(splash, 0.4f);
        Target.GetComponent<Enemy>().Solider_Damage(Archer_Damage);
    }
    void MageDamage() //마법사
    {
        GameObject splash = Instantiate(Splash, Target.transform.position, Quaternion.identity);
        splash.GetComponent<Splash>().Damage = Mage_Damage;
        Destroy(splash, 0.4f);
        Target.GetComponent<Enemy>().Solider_Damage(Mage_Damage);
    }
}