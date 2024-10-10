using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode
{
    public string Id { get; private set; }                     //스킬 노드 ID
    public string Name { get; private set; }                   //스킬 노드 이름
    public object Skill { get; private set; }                  // ?
    public List<string> RequiredSkillIds { get; private set; } //필요 스킬 ID들
    public bool isUnlocked { get; set; }                       //열렸는지 확인
    public Vector2 Position { get; set; }
    public string SkillSerires { get; private set; }
    public int SkillLevel { get; private set; }
    public string IsMaxLevel { get; set; }

    public SkillNode(string id, string name, object skill, Vector2 position, string skillSerires, int skillLevel, List<string> requiredSkillIds = null)
    {
        Id = id;
        Name = name;
        Skill = skill;
        Position = position;
        SkillSerires = skillSerires;
        RequiredSkillIds = requiredSkillIds ?? new List<string>();
        isUnlocked = false;
    }
}

public class SkillTree
{
    public List<SkillNode> Nodes { get; private set; } = new List<SkillNode> ();
    private Dictionary<string, SkillNode> nodeDictionary;

    public SkillTree()     //생성자
    {
        Nodes = new List<SkillNode>();
        nodeDictionary = new Dictionary<string, SkillNode>();
    }

    public void AddNode(SkillNode node)  //노드 추가 메서드
    {
        Nodes.Add(node);
        nodeDictionary[node.Id] = node;
    }
    
    public bool UnlockSkill(string skillId)       //스킬 잠금 해제 메서드
    {
        if (nodeDictionary.TryGetValue(skillId, out SkillNode node))
        {
            if (node.isUnlocked) return false;

            foreach(var requireSkillId in node.RequiredSkillIds)
            {
                if (!nodeDictionary[requireSkillId].isUnlocked)
                {
                    return false;
                }
            }
            node.isUnlocked = true;
            return true;
        }
        return false;
    }    

    public bool LockSkill(string skillId)           //스킬 잠금 메서드
    {
        if (nodeDictionary.TryGetValue(skillId, out SkillNode node))
        {
            if (!node.isUnlocked) return false;

            foreach (var otherNode in Nodes) //이 스킬에 의존하는 다른 스킬이 있는지 확인
            {
                if(otherNode.isUnlocked && otherNode.RequiredSkillIds.Contains(skillId))
                {
                    return false;   //의존하는 스킬이 있으면 잠금 불가능
                }
            }
            node.isUnlocked = false;
            return true;
        }

        return false;
    }

    public bool IsSkillUnlock(string skillId)
    {
        return nodeDictionary.TryGetValue(skillId, out SkillNode node) && node.isUnlocked;
    }

    public SkillNode GetNode(string skillId)
    {
        nodeDictionary.TryGetValue(skillId, out SkillNode node);
        return node;
    }

    public List<SkillNode> GetAllNodes()
    {
        return new List<SkillNode>(Nodes);
    }
}
