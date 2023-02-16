using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage; //데미지
    public float rate; //움직이는 시간
    public BoxCollider swordArea; //박스 콜라이더
    public TrailRenderer trailEffect; // 이펙트

    public void Use() //메인 루틴
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing() //코루틴(메인루틴과  코루틴 동시 실행)
    {
        yield return new WaitForSeconds(0.1f); //결과 반환 null, default 1프레임, wait 쓰면 초당 쉼
        trailEffect.enabled = true;
        swordArea.enabled = true;
        yield return new WaitForSeconds(0.2f);
        swordArea.enabled = false;
        yield return new WaitForSeconds(0.2f);
        trailEffect.enabled = false;
    }
}
