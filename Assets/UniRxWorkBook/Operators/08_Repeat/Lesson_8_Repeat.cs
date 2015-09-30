using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{
    public class Lesson_8_Repeat : MonoBehaviour
    {
        [SerializeField]
        private Text resultLabel;
        [SerializeField]
        private Button buttonLeft;
        [SerializeField]
        private Button buttonRight;

        private void Start()
        {
            var rightStream = buttonRight.OnClickAsObservable();
            var leftStream = buttonLeft.OnClickAsObservable();

            // _____を書き換え、LeftとRightを交互に1回ずつ押した時にOKが表示されるようにしよう
            // 
            // First()を外すだけではLeftとRightを連打した時の挙動が怪しいのでダメである
            // 適切なオペレータをFirstの後ろに入れよう
            leftStream
                .Zip(rightStream, (l, r) => Unit.Default)
                .First()
                ._____()
                .SubscribeToText(resultLabel, _ => resultLabel.text += "OK\n");
        }
    }
}
