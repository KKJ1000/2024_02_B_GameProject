using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetInteractPrompt();           //상호작용 시 표시할 텍스트
    void OnInteract(GameObject player);  //상호작용 시 실행될 메서드
    float GetInteractionDistance();       //상호작용 가능 거리
    bool CanInteract(GameObject player);  //상호작용 가능 여부
}
