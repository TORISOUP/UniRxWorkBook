using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.StreamSources
{

    public class Lesson_2_BehaviorSubject : MonoBehaviour
    {
        [SerializeField]
        private Button subscribeButton;
        [SerializeField]
        private Button onNextButton;
        [SerializeField]
        private Button onCompletedButton;
        [SerializeField]
        private Button resetButton;
        [SerializeField]
        private Text resultText;

        private int onNextCount = 0;

        //BehaviorSubjectは Subject に「直前の」OnNextを記憶させる機能を持たせたものである
        //Subjectとの大きな違いとしては、Subscribeした瞬間に「直前の」値を発行してくる点である
        //
        //似た挙動をするSubjectにReplaySubjectというものもあるが、こちらは全てのOnNextを記憶して再生するものである
        private BehaviorSubject<int> behaviorSubject;

        private void Start()
        {
            //BehaviorSubjectは初期値を設定できる
            behaviorSubject = new BehaviorSubject<int>(0);

            /*
            OnNext → Subscribe と実行し、Subscribeした瞬間に直前の値が発行されることを確認しよう
            */

            // Subscribeボタンが押されたらSubjectをSubscribeしてresultTextに表示する
            subscribeButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (behaviorSubject != null)
                {
                    behaviorSubject.Subscribe(
                        time => resultText.text += time.ToString() + "　", //OnNext
                        () => resultText.text += "OnCompleted　"); //OnCompleted
                }
            });

            // OnNextボタンが押されたら今が何度目のOnNextであるかを発行する
            onNextButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (behaviorSubject != null)
                {
                    behaviorSubject.OnNext(++onNextCount);
                }
            });

            // OnCompletedボタンが押されたらOnCompletedを発行する
            onCompletedButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (behaviorSubject != null)
                {
                    behaviorSubject.OnCompleted();
                }
            });

            // Resetボタンが押されたら全体を初期化する
            resetButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (behaviorSubject != null)
                {
                    behaviorSubject.OnCompleted();
                }
                behaviorSubject = new BehaviorSubject<int>(0);
                resultText.text = "";
                onNextCount = 0;
            });

        }


    }
}
