using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{

    public class Lesson_12_TakeUntil : MonoBehaviour
    {
        [SerializeField] private Button onButton;
        [SerializeField] private Button offButton;
        [SerializeField] private GameObject cube;

        private void Start()
        {
            // ____を書き換え、OFFのボタンを押したら回転が止まるようにしよう

            var onStream = onButton.OnClickAsObservable();
            var offStream = offButton.OnClickAsObservable();

            this.UpdateAsObservable()
                .SkipUntil(onStream)
                ._____()
                .RepeatUntilDestroy(gameObject)
                .Subscribe(_ => RotateCube());

        }

        private void RotateCube()
        {
            cube.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up) * cube.transform.rotation;
        }
    }
}