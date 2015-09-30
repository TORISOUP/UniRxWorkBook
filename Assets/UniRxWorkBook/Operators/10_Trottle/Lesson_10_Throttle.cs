using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{

    public class Lesson_10_Throttle : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Text resultText;

        private void Start()
        {

            // _____を書き換え、最後に文字入力されてから1秒後にresultTextに反映されるようにしてみよう
            inputField
                .OnValueChangeAsObservable()
                ._____()
                .SubscribeToText(resultText);
        }
    } 
}