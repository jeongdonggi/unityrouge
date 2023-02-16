using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //네비게이션 때 필요
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    float maxHealth;
    float curHealth;
    public int monster; // 몬스터 종류에 따라 체력 바뀜
    public Transform target;
    public Transform target_player; //플레이어 따라오는 네비
    public bool isChase;
    public bool isAttack;
    bool who = true; // 따라가는 목적 정하기
    public bool finish = true; //라스트 포인트 죽이기 전

    bool isDamages = false;

    int count; //돈 업그레이드

    Animator anim;
    Rigidbody rigid;
    public NavMeshAgent nav; // 자동 AI 네비게이션

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.Find("Finalcube").transform;
        target_player = GameObject.Find("Player").transform;

        if (monster == 1)
        {
            curHealth = 50; //마법 데미지 50
            maxHealth = 50;
        }
        else
        {
            curHealth = 60; //물리 데미지 20
            maxHealth = 60;
        }
        Invoke("ChaseStart", 0.2f);
    }
    void Update()
    {
        if (!finish)
        {
            nav.enabled = false;
        }
        if (nav.enabled)
        {
            if (who)
            {
                nav.SetDestination(target.position); //라스트 포인트 따라가기
                nav.isStopped = !isChase;
            }
            else
            {
                nav.SetDestination(target_player.position); //플레이어 따라가기
                nav.isStopped = !isChase;
            }
            
        }
        
    }

    void ChaseStart() //추격
    {
        isChase = true;
        anim.SetBool("isRun", true);
    }

    void FreezeVelocity() // 몬스터 밀림 제거
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
       
    }
    void Targeting() //AI로 찾기
    {
        float targetRadius = 1f;
        float targetRange = 1f;

        float targetRadius_player = 4f;
        float targetRange_player = 4f;

        RaycastHit[] rayHits_lastPoint = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("LastPoint"));

        RaycastHit[] rayHits_player = Physics.SphereCastAll(transform.position, targetRadius_player, transform.forward, targetRange_player, LayerMask.GetMask("Player"));
        RaycastHit[] rayHits_playerAttack = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits_lastPoint.Length > 0 && !isAttack)
        {
            who = true; //라스트 포인트 따라가기
            StartCoroutine("Attack");

        }
        else if (rayHits_player.Length > 0 && !isAttack)
        {
            who = false; //플레이어 따라가기
            if (rayHits_playerAttack.Length > 0 && !isAttack)
            {
                StartCoroutine("Attack");
            }
            
        }
        else
        {
            who = true; //라스트 포인트 따라가기
        }
    }
     IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        if (who)
        {
            this.transform.LookAt(target.transform);
        }
        else
        {
            this.transform.LookAt(target_player.transform);
        }
        
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.2f);
        if (monster == 1)
        {
            GameObject.Find("lowerarm_r").GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GameObject.Find("lower_arm.R").GetComponent<BoxCollider>().enabled = true;
        }
        
        yield return new WaitForSeconds(0.3f);
        if (monster == 1)
        {
            GameObject.Find("lowerarm_r").GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GameObject.Find("lower_arm.R").GetComponent<BoxCollider>().enabled = false;
        }
        yield return new WaitForSeconds(0.3f);
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }
    void FixedUpdate() // 몬스터 문제 해결
    {
        Targeting();
        FreezeVelocity();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isDamages)
        {
            if (other.CompareTag("Sword"))
            {
                curHealth -= GameObject.Find("Sword").GetComponent<Weapon>().damage;
                Vector3 reactVec = transform.position - GameObject.Find("Sword").GetComponent<Weapon>().transform.position; //맞고 밀림
                StartCoroutine("OnDamage");
            }
            if (other.CompareTag("Splash"))
            {
                Solider_Damage(other.GetComponent<Splash>().Damage);
            }
        } 
    }
    public void Solider_Damage(int Damage)
    {
        curHealth -= Damage;
        StartCoroutine("OnDamage");
    }

    IEnumerator OnDamage()
    {
        if(curHealth <= 0)
        {
            gameObject.layer = 12;  
            isChase = false;
            anim.SetTrigger("doDie");
            nav.enabled = false;
            Destroy(gameObject, 1.1f);
            moneyup();
            count += 1;
        }
        else
        {
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doHit");
            Invoke("falseChase", 0.7f);
            
        }
        isDamages = true;
        yield return new WaitForSeconds(0.4f);
        isDamages = false;
    }
    void falseChase()
    {
        isChase = true;
        nav.enabled = true;
    }

    void moneyup()
    {
        if (count == 0)
        {
            GameObject.Find("Money").GetComponent<Money>().money += 10; // 죽으면 돈 들어옴
            GameObject.Find("GameManager").GetComponent<gameDirecter>().Up();
        }
    }
}
