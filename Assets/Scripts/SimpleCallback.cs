using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCallback : MonoBehaviour
{
    private Action greetingAction;            //Action 선언

    void Start()
    {
        greetingAction = SayHello;            //Action 함수 할당
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
