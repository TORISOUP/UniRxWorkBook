using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{
    public class Lesson_3_SkipUntil : MonoBehaviour
    {
        private void Start()
        {
            //マウスのクリックストリーム
            var clickStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));

            // _____()の部分を正しい形に置換して、マウスのクリックが1度でも来たら回転が始まるようにしよう
            this.UpdateAsObservable()
                ._____()
                .Subscribe(_ => RotateCube());
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up) * this.transform.rotation;
        }
    }
}