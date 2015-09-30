using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{
    public class Lesson_4_Skip : MonoBehaviour
    {
        private void Start()
        {
            // _____()の部分を正しい形に置換して、マウスが3回クリックされたらCubeが回転するようにしよう
            var clickStream = this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0));

            this.UpdateAsObservable()
                .SkipUntil(clickStream._____())
                .Subscribe(_ => RotateCube());
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up) * this.transform.rotation;
        }
    }
}
