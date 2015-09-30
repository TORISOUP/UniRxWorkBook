using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{
    public class Answer_Lesson_3_SkipUntil : MonoBehaviour
    {
        private void Start()
        {
            //マウスのクリックストリーム
            var clickStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));

            // SkipUntilはシャッターのようなオペレータである
            // 引数に与えたストリーム(clickStream)に値が到達した時に、自身のシャッターを開放し本流のメッセージを後続に伝えるようになる
            // シャッターが閉じている間に届いていたメッセージはすべて破棄される
            this.UpdateAsObservable()
                .SkipUntil(clickStream)
                .Subscribe(_ => RotateCube());
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up) * this.transform.rotation;
        }
    }
}