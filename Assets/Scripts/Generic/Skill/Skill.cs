using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ų Ÿ�� �������̽�
public interface ISkillTarget
{
    void ApplyEffect(ISkillEffect effect);
}

//��ų ȿ�� �������̽�
public interface ISkillEffect
{
    void Apply(ISkillTarget target);
}

//��ü���� ȿ�� Ŭ����
public class DamageEffect : ISkillEffect
{
    public int Damage { get; private set; }
    
    public DamageEffect(int damage)
    {
        Damage = damage;
    }

    public void Apply(ISkillTarget target)
    {
        if (target is PlayerTarget playertarget)
        {
            playertarget.Health -= Damage;
            Debug.Log($"Player took {Damage} damage. Remaining health : {playertarget.Health}");
        }
        else if (target is EnemyTarget enemytarget)
        {
            enemytarget.Health -= Damage;
            Debug.Log($"Enemy took {Damage} damage. Remaining health : {enemytarget.Health}");
        }
    }
}


public class HealEffect : ISkillEffect
{
    public int HealAmount { get; private set; }

    public HealEffect(int damage)
    {
        HealAmount = damage;
    }

    public void Apply(ISkillTarget target)
    {
        if (target is PlayerTarget playertarget)
        {
            playertarget.Health += HealAmount;
            Debug.Log($"Player took {HealAmount} damage. Remaining health : {playertarget.Health}");
        }
        else if (target is EnemyTarget enemytarget)
        {
            enemytarget.Health += HealAmount;
            Debug.Log($"Enemy took {HealAmount} damage. Remaining health : {enemytarget.Health}");
        }
    }
}


//���׸� ��ų Ŭ����
public class Skill<TTarget, TEffect> 
    where TTarget : ISkillTarget
    where TEffect : ISkillEffect
{
    public string Name { get; private set; }
    public TEffect Effect { get; private set; }

    public Skill(string name, TEffect effect)
    {
        Name = name;
        Effect = effect;
    }

    public void Use(TTarget target)
    {
        Debug.Log($"Using skill: {Name}");
        target.ApplyEffect(Effect);
    }
}