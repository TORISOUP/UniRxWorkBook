using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.StreamSources
{

    public class Lesson_1_Subject : MonoBehaviour
    {
        [SerializeField] private Button subscribeButton;
        [SerializeField] private Button onNextButton;
        [SerializeField] private Button onCompletedButton;
        [SerializeField] private Button resetButton;
        [SerializeField] private Text resultText;

        private int onNextCount = 0;

        // SubjectとはIObserverとIObservableの両方を実装するクラスである。
        // つまり、SubjectはOnNext,OnCompleted,OnError,Subscribeの全てを持っており、Rxのストリームの根幹となるクラスである
        // よく使う UpdateAsObservable の定義をたどり、どういう実装になっているのかを確認するのも良いかもしれない
        private Subject<int> subject;

        private void Start()
        {
            subject = new Subject<int>();

            /*
            以下は、SubjectをGUIでいろいろいじれるようにしたものである。
            いくつか実行のパターンを記すので、この順序でボタンを押してどうなるか確認してほしい

            １．Subscribe → OnNext → OnCompleted　　　　　　　（値が発行されたあとストリームが終了する）
            ２．OnNext → Subscribe → OnNext → OnComplated 　（Subscribeしたタイミングでストリームが生成され値が受け取れるようになる）
            ３．Subscribe ×２ → OnNext → OnCompleted 　　　　 (Subscribeを実行した数だけストリームが生成され稼働する）
            */

            // Subscribeボタンが押されたらSubjectをSubscribeしてresultTextに表示する
            subscribeButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (subject != null)
                {
                    subject.Subscribe(
                        time => resultText.text += time.ToString() + "　", //OnNext
                        () => resultText.text += "OnCompleted　"); //OnCompleted
                }
            });

            // OnNextボタンが押されたら今が何度目のOnNextであるかを発行する
            onNextButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (subject != null)
                {
                    subject.OnNext(++onNextCount);
                }
            });

            // OnCompletedボタンが押されたらOnCompletedを発行する
            onCompletedButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (subject != null)
                {
                    subject.OnCompleted();
                }
            });

            // Resetボタンが押されたら全体を初期化する
            resetButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (subject != null)
                {
                    subject.OnCompleted();
                }
                subject = new Subject<int>();
                resultText.text = "";
                onNextCount = 0;
            });

        }


    }
}
