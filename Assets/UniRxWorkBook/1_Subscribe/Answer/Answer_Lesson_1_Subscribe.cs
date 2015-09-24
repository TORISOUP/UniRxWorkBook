using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook
{

    public class Answer_Lesson_1_Subscribe : MonoBehaviour
    {
        private void Start()
        {
            // UniRx.Triggerを追加することでthis.UpdateAsObservable()でUpdate()をストリームとして扱うことができるようになります
            // あとはそれをSubscribeし、毎フレームCubeが回転するようにします。
            this.UpdateAsObservable().Subscribe(_ => RotateCube());

            //ちなみにラムダ式の左辺に _ を使っていますが、これは「デリゲート内でこの引数を使うことはない」という意味です
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up)*this.transform.rotation;
        }
    }

}
