using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook
{
    public class Answer_Lesson_3_SkipUntil : MonoBehaviour
    {
        private void Start()
        {
            //マウスのクリックストリーム
            var clickStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));

            // SkipUntilはシャッターのようなオペレータです
            // 引数に与えたストリーム(clickStream)に値が到達した時に、自身のシャッターを開放し本流のメッセージを後続に伝えます
            // シャッターが閉じている間に届いていたメッセージはすべて破棄されます
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