using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook
{

    public class Answer_Lesson_9_CombineLatest : MonoBehaviour
    {
        [SerializeField]
        private InputField leftInput;
        [SerializeField]
        private InputField rightInput;
        [SerializeField]
        private Text resultLabel;
        private void Start()
        {
            var leftStream = leftInput.OnValueChangeAsObservable().Select(x => Int32.Parse(x));
            var rightStream = rightInput.OnValueChangeAsObservable().Select(x => Int32.Parse(x));

            // ZipをCombineLatestに書き換えれば良い
            // Zipは複数のストリームのメッセージが1つずつ揃った時に放出するものであったが、
            // CombineLatestはすべてのストリームの値が揃ってなくても直前の値を代わりに使って放出するオペレータである
            // [参考] http://reactivex.io/documentation/operators/combinelatest.html
            leftStream
                .CombineLatest(rightStream, (left, right) => left + right)
                .SubscribeToText(resultLabel);
        }
    }
}
