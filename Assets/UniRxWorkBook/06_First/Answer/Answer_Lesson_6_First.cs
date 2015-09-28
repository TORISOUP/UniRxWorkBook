using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook
{
    public class Answer_Lesson_6_First : MonoBehaviour
    {

        [SerializeField]
        private Text resultLabel;
        [SerializeField]
        private Button buttonA;
        [SerializeField]
        private Button buttonB;
        [SerializeField]
        private Button buttonC;

        private void Start()
        {
            var aStream = buttonA.OnClickAsObservable().Select(_ => "A");
            var bStream = buttonB.OnClickAsObservable().Select(_ => "B");
            var cStream = buttonC.OnClickAsObservable().Select(_ => "C");

            // First,FirstOrDefault を使うとこで一番最初のメッセージのみを通過させることができる
            // 一番最初のメッセージをを通過させた後、FirstはOnCompletedを放出する点に注意
            // 
            // FirstとFirstOrDefaultでは、メッセージが一度も発行されることなくDisposeされた時、
            // Firstは [OnError] を発行するが　FirstOrDefault　は [OnNext + OnCompleted] を発行するという違いがある。
            // 用途に合わせ、適切な方を選択するべきである
            Observable.Merge(aStream, bStream, cStream)
                .First()
                .SubscribeToText(resultLabel);
        }
    }
}