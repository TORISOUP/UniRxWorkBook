using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

namespace UniRxWorkBook.StreamSources
{

    public class Lesson_3_ReplaySubject : MonoBehaviour
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

        // ReplaySubjectは過去のOnNextを全て記憶し、Subscribe時に全てまとめて発行する機能をもつSubjectである
        // BehaviorSubjectとの違いは、BehaviorSubjectは直前の値のみをキャッシュするものであり、ReplaySubjectは過去全てをキャッシュする
        private ReplaySubject<int> replaySubject;

        private void Start()
        {
            //ReplaySubjectも初期を設定することができる（今回は設定しない）
            replaySubject = new ReplaySubject<int>();

            /*
            OnNextを何回か繰り返す → Subscribe と実行し、Subscribeした瞬間に過去の値がまとめて値が発行されることを確認しよう
            */

            // Subscribeボタンが押されたらSubjectをSubscribeしてresultTextに表示する
            subscribeButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (replaySubject != null)
                {
                    replaySubject.Subscribe(
                        time => resultText.text += time.ToString() + "　", //OnNext
                        () => resultText.text += "OnCompleted　"); //OnCompleted
                }
            });

            // OnNextボタンが押されたら今が何度目のOnNextであるかを発行する
            onNextButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (replaySubject != null)
                {
                    replaySubject.OnNext(++onNextCount);
                }
            });

            // OnCompletedボタンが押されたらOnCompletedを発行する
            onCompletedButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (replaySubject != null)
                {
                    replaySubject.OnCompleted();
                }
            });

            // Resetボタンが押されたら全体を初期化する
            resetButton.OnClickAsObservable().Subscribe(_ =>
            {
                if (replaySubject != null)
                {
                    replaySubject.OnCompleted();
                }
                replaySubject = new ReplaySubject<int>();
                resultText.text = "";
                onNextCount = 0;
            });

        }


    }
}
