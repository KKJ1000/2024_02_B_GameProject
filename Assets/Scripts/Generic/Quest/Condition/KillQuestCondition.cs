using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
    //���� óġ ����Ʈ�� ������ �����ϴ� Ŭ����
    public class KillQuestCondition : IQuestCondition
    {
        private string enemyType; //óġ�ؾ� �� ���� ����
        private int requiredKills; //óġ�ؾ� �� �� ���� ��
        private int currentKills;     //������� óġ�� ���� ��

        //óġ ����Ʈ ���� �ʱ�ȭ ������
        public KillQuestCondition(string enemyType, int requiredKills)
        {
            this.enemyType = enemyType;
            this.requiredKills = requiredKills;
            this.currentKills = 0;
        }

        public bool IsMet() => currentKills >= requiredKills; //��ǥ óġ ���� �޼� �ߴ��� Ȯ��
        public void Initialize() => currentKills = 0;         //óġ ���� 0���� �ʱ�ȭ
        public float GetProgress() => (float)currentKills / requiredKills;  //���� óġ ���൵�� �ۼ�Ʈ�� ��ȯ
        //����Ʈ ���� ������ ���ڿ��� ��ȯ
        public string GetDescription() => $"Defeat{requiredKills} {enemyType} ({currentKills}/{requiredKills})"; 

        public void EnemyKilled(string enemyType)  //�� óġ �� ȣ��Ǵ� �޼���
        {
            if(this.enemyType == enemyType)
            {
                currentKills++;
            }
        }
    }
}

