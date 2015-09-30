using UnityEngine;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{
    public class Answer_Lesson_5_Buffer : MonoBehaviour
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

            // Buffer(n)で、過去n個分のメッセージをバッファすることができる
            // 何個溜まったら放出するかは第二引数で指定することができる（デフォルトは第一引数と同じ値がセットされる）
            //
            // Buffer(3)をBuffer(3,1)に変えて挙動の違いを確認してみよう
            Observable.Merge(aStream, bStream, cStream)
                .Buffer(3)
                .SubscribeToText(resultLabel, x => x.Aggregate((p, c) => p + c));
        }

    }
}