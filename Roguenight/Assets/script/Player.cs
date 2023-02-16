using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool sDown;

    bool fDown;
    float fireDelay = 0;
    bool isfireReady = true;

    Vector3 moveVec;

    public float maxHealth = 200;
    public float curHealth = 200;

    Animator anim;
    Rigidbody rigid;
    bool isBorder;

    public GameObject PlayerHp;

    public Enemy ene;

    bool isDamages = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PlayerHp = GameObject.Find("PlayerHp");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Attack();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); //좌우
        vAxis = Input.GetAxisRaw("Vertical"); //상하
        sDown = Input.GetButton("Sprint"); //쉬프트 누르면 스프린트
        fDown = Input.GetButtonDown("Attack"); //z누르면 공격
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //움직임

        if (!isfireReady)
        {
            moveVec = Vector3.zero;
        }

        if (!isBorder)
        {
            transform.position += moveVec * speed * (sDown ? 1f : 0.7f) * Time.deltaTime; //삼항연산자로 속도 구현
        }
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isSprint", sDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec); //회전 함수
    }

    void Attack()
    {
        fireDelay += Time.deltaTime; //매 프레임 소비한 시간
        if (GameObject.Find("Sword").GetComponent<Weapon>().rate < fireDelay)
        {
            isfireReady = true;//칼이 움직이는 속도가 더 작을 때 공격 가능
        }
        else //공격 중임, 칼이 움직이는 시간이 0.4인데 그것보다 짧기 때문
        {
            isfireReady = false;
        }
        if (fDown && isfireReady)
        {
            GameObject.Find("Sword").GetComponent<Weapon>().Use(); //조건이 충족되면 함수 실행
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
    }
    void FreezeRotation() // 캐릭터가 물체에 맞고 스스로 도는 리지드를 고침
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void StopToWall() //충돌 방지
    {
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }
    void FixedUpdate() // 캐릭터 문제 해결
    {
        FreezeRotation();
        StopToWall();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isDamages)
        {
            if (other.CompareTag("EnemyWeapon"))
            {
                curHealth -= GameObject.Find("lowerarm_r").GetComponent<BornenemyWeapon>().damage;
                PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
                Vector3 reactVec = transform.position - GameObject.Find("lowerarm_r").GetComponent<BornenemyWeapon>().transform.position; //맞고 밀림
                StartCoroutine("OnDamage");
            }
            if (other.CompareTag("zombieWeapon"))
            {
                curHealth -= GameObject.Find("lower_arm.R").GetComponent<BornenemyWeapon>().damage;
                PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
                Vector3 reactVec = transform.position - GameObject.Find("lower_arm.R").GetComponent<BornenemyWeapon>().transform.position; //맞고 밀림
                StartCoroutine("OnDamage");
            }
            else if (other.CompareTag("BossWeapon"))
            {
                curHealth -= GameObject.Find("sword").GetComponent<BornenemyWeapon>().damage;
                PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
                Vector3 reactVec = transform.position - GameObject.Find("sword").GetComponent<BornenemyWeapon>().transform.position; //맞고 밀림
                StartCoroutine("OnDamage");
            }
        }
    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(0.1f);
        if (curHealth <= 0)
        {
            anim.SetTrigger("doDie");
            Invoke("gone", 1f);
        }
        else
        {
            anim.SetTrigger("doHit");
            yield return new WaitForSeconds(0.7f);
        }
        isDamages = true;
        yield return new WaitForSeconds(0.4f);
        isDamages = false;
    }
    void gone()
    {
        transform.position = new Vector3(-1.5f, 0, 198.5f);
        Debug.Log("옮김");
    }
    public void idle()
    {
        transform.position = new Vector3(0f, 0, 30f);
        anim.SetBool("isIdle",true);
        curHealth = maxHealth;
        PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
    }
}
