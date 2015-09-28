using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook
{

    public class Answer_Lesson_10_Throttle : MonoBehaviour
    {
        [SerializeField]
        private InputField inputField;
        [SerializeField]
        private Text resultText;

        private void Start()
        {

            // Throttleはメッセージが連続した時に、落ち着くまで待つオペレータ
            //
            // 指定した時間より短い間隔でメッセージが大量に来た場合、メッセージをすべて無視する
            // そして指定時間以上経過したときに、受け取ったメッセージのうち一番最後に来ていたものを１つだけ放出する
            // 今回のケースの様に、確定するまで値がブレたりするものに使うことで「安定するまで待つ」といった処理が簡単に書ける
            //
            // なお、フレーム数を指定できるThrottleFrameもある
            inputField
                .OnValueChangeAsObservable()
                .Throttle(TimeSpan.FromSeconds(1))
                .SubscribeToText(resultText);
        }
    }
}
