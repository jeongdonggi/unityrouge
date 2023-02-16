using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : MonoBehaviour
{
    public int number;
    public float Range; //���� ����
    int Archer_Damage = 20;
    int Mage_Damage = 50;
    public GameObject Soldier; // ���� ������Ʈ �޾��ֱ�
    public GameObject Splash; //���÷���

    GameObject Target; // ����
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
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Enemy"); //���ʹ� �ϴ� �� ����Ʈ�� �ֱ�
        float shortestDistance = Mathf.Infinity; //���� ª�� �Ÿ�( �ִ����� �ʱ�ȭ)
        GameObject nearestMonster = null; // ���� ����� ����
        //foreach�� : �迭�� ���ʷ� �����Ͽ� ������ ����ش�.
        foreach (GameObject Monster in Monsters)
        { //������ Monster ������Ʈ�� ���� �迭�� �־���
            float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position); // Ÿ���� ���� ������ �Ÿ� ���ϱ�
            if (DistanceToMonsters < shortestDistance)
            {
                shortestDistance = DistanceToMonsters; //���� ���� �Ÿ� ���� �� ���� �Ÿ��� ���� ����ؼ� ���ϱ� ����
                nearestMonster = Monster; //�̋��� ���͸� �־��ش�.,
            }
        }
        if (nearestMonster != null && shortestDistance <= Range) // ���Ͱ� �ְ� ���� ������ ������ �ȴٸ�
        {
            Target = nearestMonster; // Ÿ���� ����� ����
            Attack();
        }
        else
        {
            Idle();
            Target = null; // �̸��� ���ų� ������ ������ ������������.
        }
    }

    void Attack() //����
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
    void Idle() // ���� ¥�ߵ�
    {
        anim.SetBool("isAttack", false);
        if (number == 1) //��ó
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void ArcherDamage() //�ϴ� ���
    {
        GameObject splash = Instantiate(Splash, Target.transform.position, Quaternion.identity);
        splash.GetComponent<Splash>().Damage = Archer_Damage;
        Destroy(splash, 0.4f);
        Target.GetComponent<Enemy>().Solider_Damage(Archer_Damage);
    }
    void MageDamage() //������
    {
        GameObject splash = Instantiate(Splash, Target.transform.position, Quaternion.identity);
        splash.GetComponent<Splash>().Damage = Mage_Damage;
        Destroy(splash, 0.4f);
        Target.GetComponent<Enemy>().Solider_Damage(Mage_Damage);
    }
}