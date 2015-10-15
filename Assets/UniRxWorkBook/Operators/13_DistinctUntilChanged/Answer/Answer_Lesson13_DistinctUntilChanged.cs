using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace UniRxWorkBook
{
    public class Answer_Lesson_13_DistinctUntilChanged : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;
        [SerializeField]
        private Text statusText;

        private void Start()
        {
            var controller = target.GetComponent<CharacterController>();

            /*
            DistinctUntilChangedはメッセージの値に変動があった時のみ、メッセージを通過させるオペレータである。

            なお、今回のケースの様に、何かの値を毎フレーム監視し変化があった時のみ処理をするといったことは開発する上で頻出する。
            そこで 「Observable.EveryUpdate().Select().DistinctUntilChanged()」までの処理をセットにしたObserveEveryValueChangedというものもUniRxには用意されている。
            */

            this.UpdateAsObservable()
                .Select(_ => controller.isGrounded)
                .DistinctUntilChanged()
                .Subscribe(isGrounded => StatusOutput(isGrounded));

            
            //ObserveEveryValueChangedを使うことで、この設問のコードは以下の様に書くこともできる。
            controller.ObserveEveryValueChanged(x => x.isGrounded)
                .Subscribe(isGrounded => StatusOutput(isGrounded));

            /*
             ただし、ObserveEveryValueChangedを使った場合のストリームの寿命の管理には気をつける必要がある。

             上記の書き方の場合、ストリームの寿命は「controllerインスタンス」の寿命と同じになる。
             つまり、このgameObjectが破棄されたとしても、controllerが残っている限り裏でストリームは動作を続ける状態になってしまう。
             そのためAddToなどで寿命を管理し、ストリームがどのオブジェクトが破棄された段階で止めたいのかを明示しておくべきである。
            */

            controller.ObserveEveryValueChanged(x => x.isGrounded)
                .Subscribe(isGrounded => StatusOutput(isGrounded))
                .AddTo(this.gameObject); //this.gameObjectが破棄されたらDisposeされるようにする

        }

        #region Output
        private List<string> logList = new List<string>();
        private int maxLogLength = 20;

        /// <summary>
        /// 着地または離陸の状態を文字列に変換して表示する
        /// </summary>
        /// <param name="isGrounded"></param>
        private void StatusOutput(bool isGrounded)
        {
            var text = isGrounded ? "OnGrounded\n" : "OnJumped\n";
            AddLog(text);
            statusText.text = logList.Aggregate((p, c) => p + c);
        }

        /// <summary>
        /// ログのテキストを先頭に追加する
        /// </summary>
        private void AddLog(string text)
        {
            logList.Insert(0, text);
            if (logList.Count > maxLogLength) logList.RemoveAt(maxLogLength);
        }
        #endregion
    }
}