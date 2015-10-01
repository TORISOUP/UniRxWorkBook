using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.StreamSources
{

    public class Lesson_4_AsyncSubject : MonoBehaviour
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

        // AsyncSubjectは非同期処理を模したSubjectである
        // OnNextが発行されるたびに最新の値をキャッシュし、OnCompletedの実行時に最新のOnNextを１つだけ通知する
        // （非同期で処理を回し、非同期処理完了を持って最後の結果を通知するイメージである）
        private AsyncSubject<int> asyncSubject;

        private void Start()
        {
            //BehaviorSubjectは初期値を設定できる
            asyncSubject = new AsyncSubject<int>();

            /*
            Subscribe後にOnNextを繰り返しても値が発行されず、OnCompletedを実行した際に初めてOnNextが通知されるところ確認しよう
            また、その時のOnNextの値は最後の値１つだけになっていることも確認しよう
            */

            // Subscribeボタンが押されたらSubjectをSubscribeしてresultTextに表示する
            subscribeButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (asyncSubject != null)
                {
                    asyncSubject.Subscribe(
                        time => resultText.text += time.ToString() + "　", //OnNext
                        () => resultText.text += "OnCompleted　"); //OnCompleted
                }
            });

            // OnNextボタンが押されたら今が何度目のOnNextであるかを発行する
            onNextButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (asyncSubject != null)
                {
                    asyncSubject.OnNext(++onNextCount);
                }
            });

            // OnCompletedボタンが押されたらOnCompletedを発行する
            onCompletedButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (asyncSubject != null)
                {
                    asyncSubject.OnCompleted();
                }
            });

            // Resetボタンが押されたら全体を初期化する
            resetButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (asyncSubject != null)
                {
                    asyncSubject.OnCompleted();
                }
                asyncSubject = new AsyncSubject<int>();
                resultText.text = "";
                onNextCount = 0;
            });

        }


    }
}
