using UnityEngine;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{
    public class Lesson_6_First : MonoBehaviour
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

            // _____を書き換え、最初の１回目が押された時のみTextが書き換わるようにしよう
            Observable.Merge(aStream, bStream, cStream)
                ._____()
                .SubscribeToText(resultLabel);
        }

    }
}