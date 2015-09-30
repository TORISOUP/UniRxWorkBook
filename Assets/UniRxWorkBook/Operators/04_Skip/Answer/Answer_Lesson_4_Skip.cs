using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{
    public class Answer_Lesson_4_Skip : MonoBehaviour
    {
        private void Start()
        {
            var clickStream = this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0));

            //Skip(n)はn回メッセージが到着するまでメッセージを遮断するオペレータ
            //3回目でメッセージが通るようにしたいので、この場合は最初の2回をSkipさせれば良い
            this.UpdateAsObservable()
                .SkipUntil(clickStream.Skip(2))
                .Subscribe(_ => RotateCube());
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up) * this.transform.rotation;
        }
    }
}
