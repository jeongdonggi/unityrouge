using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage; //������
    public float rate; //�����̴� �ð�
    public BoxCollider swordArea; //�ڽ� �ݶ��̴�
    public TrailRenderer trailEffect; // ����Ʈ

    public void Use() //���� ��ƾ
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing() //�ڷ�ƾ(���η�ƾ��  �ڷ�ƾ ���� ����)
    {
        yield return new WaitForSeconds(0.1f); //��� ��ȯ null, default 1������, wait ���� �ʴ� ��
        trailEffect.enabled = true;
        swordArea.enabled = true;
        yield return new WaitForSeconds(0.2f);
        swordArea.enabled = false;
        yield return new WaitForSeconds(0.2f);
        trailEffect.enabled = false;
    }
}
