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
        hAxis = Input.GetAxisRaw("Horizontal"); //�¿�
        vAxis = Input.GetAxisRaw("Vertical"); //����
        sDown = Input.GetButton("Sprint"); //����Ʈ ������ ������Ʈ
        fDown = Input.GetButtonDown("Attack"); //z������ ����
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //������

        if (!isfireReady)
        {
            moveVec = Vector3.zero;
        }

        if (!isBorder)
        {
            transform.position += moveVec * speed * (sDown ? 1f : 0.7f) * Time.deltaTime; //���׿����ڷ� �ӵ� ����
        }
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isSprint", sDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec); //ȸ�� �Լ�
    }

    void Attack()
    {
        fireDelay += Time.deltaTime; //�� ������ �Һ��� �ð�
        if (GameObject.Find("Sword").GetComponent<Weapon>().rate < fireDelay)
        {
            isfireReady = true;//Į�� �����̴� �ӵ��� �� ���� �� ���� ����
        }
        else //���� ����, Į�� �����̴� �ð��� 0.4�ε� �װͺ��� ª�� ����
        {
            isfireReady = false;
        }
        if (fDown && isfireReady)
        {
            GameObject.Find("Sword").GetComponent<Weapon>().Use(); //������ �����Ǹ� �Լ� ����
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
    }
    void FreezeRotation() // ĳ���Ͱ� ��ü�� �°� ������ ���� �����带 ��ħ
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void StopToWall() //�浹 ����
    {
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }
    void FixedUpdate() // ĳ���� ���� �ذ�
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
                Vector3 reactVec = transform.position - GameObject.Find("lowerarm_r").GetComponent<BornenemyWeapon>().transform.position; //�°� �и�
                StartCoroutine("OnDamage");
            }
            if (other.CompareTag("zombieWeapon"))
            {
                curHealth -= GameObject.Find("lower_arm.R").GetComponent<BornenemyWeapon>().damage;
                PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
                Vector3 reactVec = transform.position - GameObject.Find("lower_arm.R").GetComponent<BornenemyWeapon>().transform.position; //�°� �и�
                StartCoroutine("OnDamage");
            }
            else if (other.CompareTag("BossWeapon"))
            {
                curHealth -= GameObject.Find("sword").GetComponent<BornenemyWeapon>().damage;
                PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
                Vector3 reactVec = transform.position - GameObject.Find("sword").GetComponent<BornenemyWeapon>().transform.position; //�°� �и�
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
        Debug.Log("�ű�");
    }
    public void idle()
    {
        transform.position = new Vector3(0f, 0, 30f);
        anim.SetBool("isIdle",true);
        curHealth = maxHealth;
        PlayerHp.GetComponent<Image>().fillAmount = curHealth / maxHealth;
    }
}
