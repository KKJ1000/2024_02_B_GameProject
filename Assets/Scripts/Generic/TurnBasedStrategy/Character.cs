using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    //캐릭터 스탯
    public bool isPlayer;      //플레이어 여부
    public int hp = 100;       //플레이어 HP
    public int speed;          //플레이어 속도
     
    //UI 요소
    TextMeshProUGUI nameText;  //이름 표시
    TextMeshProUGUI hpText;    //HP 표시
    Vector3 startPos;          //시작 위치 (공격 애니메이션 용)

    void Start()
    {
        SetupNameText();                //UI 텍스트 초기화
        startPos = transform.position;  //시작 위치 저장
    }

    void OnDisable()
    {
        if (nameText != null) Destroy(nameText.gameObject);
        if (hpText != null) Destroy(hpText.gameObject);
    }

    //UI 텍스트 초기화
    void SetupNameText()
    {
        //이름 텍스트 설정
        GameObject textObj = new GameObject("NameText");
        textObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        nameText = textObj.AddComponent<TextMeshProUGUI>();
        nameText.text = isPlayer ? "P" : "E";
        nameText.fontSize = 36;
        nameText.alignment = TextAlignmentOptions.Center;

        //HP 텍스트 설정
        GameObject hpObj = new GameObject("HpText");
        hpObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        hpText = hpObj.AddComponent<TextMeshProUGUI>();
        hpText.fontSize = 30;
        hpText.alignment = TextAlignmentOptions.Center;
    }

    //공격 함수
    public void Attack(Character target)
    {
        if (!target.gameObject.activeSelf) return; //죽어있는 타겟이면 무시
        StartCoroutine(AttackRoutine(target));     //공격애니메이션 시작
    }

    private void Update()
    {
        //UI 위치 업데이트
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        nameText.transform.position = screenPos;
        hpText.transform.position = screenPos + Vector3.down * 30;

        //현재 턴 캐릭터 표시 (초록색) 배틀시스템 구현 이후 구현
        nameText.color = (BattleSystem.Instance.GetCurrentChar() == this) ? Color.green : Color.white;
        hpText.text = hp.ToString();
    }

    //공격 애니메이션 코루틴
    IEnumerator AttackRoutine(Character target)
    {
        //타겟쪽으로 이동
        Vector3 attackPos = target.transform.position + (target.transform.position - transform.position).normalized * 1.5f;
        float moveTime = 0.3f;
        float elapsed = 0;

        //이동 애니메이션
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(startPos, attackPos, t);
            yield return null;
        }

        //데미지 처리
        target.hp -= 20;
        if (target.hp <= 0) target.gameObject.SetActive(false);

        //복귀 애니메이션 (원위치로 이동)
        elapsed = 0;
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(attackPos, startPos, t);
            yield return null;
        }
        transform.position = startPos;
    }
}
