using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook
{

    public class Answer_Lesson_11_ThrottleFirst : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletObject;

        private void Start()
        {
            // ThrottleFirstはThrottleの逆で、「最初にメッセージが来てから一定期間メッセージを遮断する」オペレータである
            // これを使うことで多すぎるメッセージを簡単に間引くことができる
            // なお、フレーム数を指定できるThrottleFirstFrameもある
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButton(0))
                .ThrottleFirst(TimeSpan.FromMilliseconds(100)) //１つメッセージが来てから100ms間メッセージを遮断する
                .Subscribe(_ =>
                {
                    var b = Instantiate(bulletObject, transform.position, Quaternion.identity) as GameObject;
                    Destroy(b, 2.0f);
                });
        }
    }
}
