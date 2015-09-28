using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook
{
    public class Answer_Lesson_7_Zip : MonoBehaviour
    {

        [SerializeField] private Text resultLabel;
        [SerializeField] private Button buttonLeft;
        [SerializeField] private Button buttonRight;

        private void Start()
        {
            var rightStream = buttonRight.OnClickAsObservable();
            var leftStream = buttonLeft.OnClickAsObservable();

            // Zipは複数のオペレータの値を１つずつまとめて流すオペレータである
            // 今回は2本のストリームを指定しているので、2本のストリームの両方のメッセージが1つずつ揃った時にメッセージを放流する
            // その際、第二引数に与えるデリゲートで値の加工が可能である（今回は値を使わないのでUnitを流している）
            //
            // また、Zipは内部にBufferいる点も重要である
            // 例えば以下のFirstを削除した状態で実行し、Leftを連続で3回押した後に、Rightを連続で3回押してみよう
            // (実行された回数がわかりやすいようにTextの更新を追記する形にしておく）
            leftStream
                .Zip(rightStream, (l, r) => Unit.Default)
                .First()
                .SubscribeToText(resultLabel, _ => resultLabel.text += "OK\n");
        }

    }
}