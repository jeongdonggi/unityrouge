using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //�׺���̼� �� �ʿ�
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    float maxHealth;
    float curHealth;
    public int monster; // ���� ������ ���� ü�� �ٲ�
    public Transform target;
    public Transform target_player; //�÷��̾� ������� �׺�
    public bool isChase;
    public bool isAttack;
    bool who = true; // ���󰡴� ���� ���ϱ�
    public bool finish = true; //��Ʈ ����Ʈ ���̱� ��

    bool isDamages = false;

    int count; //�� ���׷��̵�

    Animator anim;
    Rigidbody rigid;
    public NavMeshAgent nav; // �ڵ� AI �׺���̼�

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.Find("Finalcube").transform;
        target_player = GameObject.Find("Player").transform;

        if (monster == 1)
        {
            curHealth = 50; //���� ������ 50
            maxHealth = 50;
        }
        else
        {
            curHealth = 60; //���� ������ 20
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
                nav.SetDestination(target.position); //��Ʈ ����Ʈ ���󰡱�
                nav.isStopped = !isChase;
            }
            else
            {
                nav.SetDestination(target_player.position); //�÷��̾� ���󰡱�
                nav.isStopped = !isChase;
            }
            
        }
        
    }

    void ChaseStart() //�߰�
    {
        isChase = true;
        anim.SetBool("isRun", true);
    }

    void FreezeVelocity() // ���� �и� ����
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
       
    }
    void Targeting() //AI�� ã��
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
            who = true; //��Ʈ ����Ʈ ���󰡱�
            StartCoroutine("Attack");

        }
        else if (rayHits_player.Length > 0 && !isAttack)
        {
            who = false; //�÷��̾� ���󰡱�
            if (rayHits_playerAttack.Length > 0 && !isAttack)
            {
                StartCoroutine("Attack");
            }
            
        }
        else
        {
            who = true; //��Ʈ ����Ʈ ���󰡱�
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
    void FixedUpdate() // ���� ���� �ذ�
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
                Vector3 reactVec = transform.position - GameObject.Find("Sword").GetComponent<Weapon>().transform.position; //�°� �и�
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
            GameObject.Find("Money").GetComponent<Money>().money += 10; // ������ �� ����
            GameObject.Find("GameManager").GetComponent<gameDirecter>().Up();
        }
    }
}
