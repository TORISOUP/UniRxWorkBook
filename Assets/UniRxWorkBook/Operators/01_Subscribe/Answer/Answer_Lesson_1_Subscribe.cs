using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{

    public class Answer_Lesson_1_Subscribe : MonoBehaviour
    {
        private void Start()
        {
            // UniRx.Triggerを追加することでthis.UpdateAsObservable()でUpdate()をストリームとして扱うことができるようになる
            // あとはそれをSubscribeし、毎フレームCubeが回転するようになる
            this.UpdateAsObservable().Subscribe(_ => RotateCube());

            //ちなみにラムダ式の左辺に _ を使っているが、これは「デリゲート内でこの引数を使うことはない」という意味である
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up)*this.transform.rotation;
        }
    }

}
