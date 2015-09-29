using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace UniRxWorkBook
{

    public class Answer_Lesson_12_TakeUntil : MonoBehaviour
    {
        [SerializeField]
        private Button onButton;
        [SerializeField]
        private Button offButton;
        [SerializeField]
        private GameObject cube;

        private void Start()
        {
            // TakeUntil は引数で与えたストリームにメッセージが到達した際にOnCompletedを発行する
            // SkiptUntilと組み合わせることで、「イベントAが来てからイベントBが来るまで処理する」といったことが簡単に記述できる
            var onStream = onButton.OnClickAsObservable();
            var offStream = offButton.OnClickAsObservable();
            this.UpdateAsObservable()
                .SkipUntil(onStream)
                .TakeUntil(offStream)
                .RepeatUntilDestroy(gameObject)
                .Subscribe(_ => RotateCube());

            // なお、TakeUntilは本流にメッセージが到達しているか否かに関係なく動作する点に注意するべきである
            // たとえば、Repeatを外して
            //
            // this.UpdateAsObservable()
            //    .SkipUntil(onStream)
            //    .TakeUntil(offStream)
            //    .Subscribe(_ => RotateCube(), () => Debug.Log("OnComplted!"));
            //
            // このような構成にした状態で、OFFボタンを先に押してみよう。ログに「OnComplted！」と表示されるはずである。
            // SkipUntilによってメッセージが阻まれ、TakeUntilにまだメッセージが到達していないにもかかわらずTakeUntilが発動してしまった。
            //
            // このように、TakeUntilはSubscribeされた瞬間に稼働を始め、OnNextが到達したかどうかは関係ないという点を覚えて多く必要がある。
            // (　✕：OnNextメッセージが来た瞬間に有効になる　○：Subscribeした瞬間に既に有効になっている） 
        }

        private void RotateCube()
        {
            cube.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up) * cube.transform.rotation;
        }
    }
}