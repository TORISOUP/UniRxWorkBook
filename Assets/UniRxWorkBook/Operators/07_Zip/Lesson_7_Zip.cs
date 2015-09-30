using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{
    public class Lesson_7_Zip : MonoBehaviour
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

            // _____を書き換え、LeftとRightが両方共最低１回ずつ押された時にTextが書き換わるようにしてみよう
            leftStream
                ._____()
                .First()
                .SubscribeToText(resultLabel, _ => "OK");
        }

    }
}