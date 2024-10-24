using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
    public class ExperienceReward : IQuestReward
    {
        private int experienceAmount;     //�������� ������ ����ġ��
        
        public ExperienceReward(int amount)  //����ġ ���� �ʱ�ȭ ������
        {
            this.experienceAmount = amount;
        }

        public void Grant(GameObject player)
        {
            //TODO : ���� ����ġ ���� ���� ����
            Debug.Log($"Granted {experienceAmount} experience");
        }

        public string GetDescription() => $"{experienceAmount} Experience Points";
    }
}

