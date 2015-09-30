using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{

    public class Lesson_2_Where : MonoBehaviour
    {

        private void Start()
        {
            // _____()の部分を正しい形に置換して、マウスの左クリックをしている間のみCubeが回転するようにしよう
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
