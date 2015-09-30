using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{
    public class Lesson_9_CombineLatest : MonoBehaviour
    {
        [SerializeField] private InputField leftInput;
        [SerializeField] private InputField rightInput;
        [SerializeField] private Text resultLabel;
        private void Start()
        {
            var leftStream = leftInput.OnValueChangeAsObservable().Select(x => Int32.Parse(x));
            var rightStream = rightInput.OnValueChangeAsObservable().Select(x => Int32.Parse(x));

            // 下記のオペレータチェーンは2つのInputFieldに入力された数値を合計して表示するストリームを生成している
            // ただ、Zipでは挙動がおかしいので、Zipを適切なオペレータに変更しInputFiledの変更が即時反映されるようにしよう
            leftStream
                .Zip(rightStream, (left, right) => left + right)
                .SubscribeToText(resultLabel);
        }
    }
}