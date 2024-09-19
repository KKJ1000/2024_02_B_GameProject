using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCallback : MonoBehaviour
{
    private Action greetingAction;            //Action ����

    void Start()
    {
        greetingAction = SayHello;            //Action �Լ� �Ҵ�
        PerformGreeting(greetingAction);
    }

    void SayHello()
    {
        Debug.Log("Hello, World");
    }

    void PerformGreeting(Action greetingFunc)
    {
        greetingFunc?.Invoke();
    }

}
