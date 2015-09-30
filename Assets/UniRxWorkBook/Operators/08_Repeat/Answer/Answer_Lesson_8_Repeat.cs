using UnityEngine;
using System.Collections;
using UniRx;
using UniRxWorkBook;
using UnityEngine.UI;

namespace UniRxWorkBook.Operators
{
    public class Answer_Lesson_8_Repeat : MonoBehaviour
    {

        [SerializeField] private Text resultLabel;
        [SerializeField] private Button buttonLeft;
        [SerializeField] private Button buttonRight;

        private void Start()
        {
            var rightStream = buttonRight.OnClickAsObservable();
            var leftStream = buttonLeft.OnClickAsObservable();

            // Repeat はOnCompletedが発行された時に、再度ストリームをSubscribeし直してくれるオペレータである
            // (流れてきた値を再現して繰り返すような機能ではないので注意）
            // 引数に数値を与えることで、指定した回数のみ動作させることもできる（デフォルトは無限動作）
            //
            // ！！！！Repeatは危険なオペレータであるということを認識しなくてはいけない！！！
            // 下手に使うと無限Subscribeを引き起こしフリーズを引き起こす可能性がある
            // 特にシーン遷移時など、オブジェクトが破棄されOnCompletedが発行されるタイミングには注意が必要である
            //
            // UniRxには、
            // 短期間にRepeatが連発した時にDisposeする　【RepeatSafe】
            // GameObjectがDisableになったタイミングでDisposeする　【RepeatUntilDisable】
            // GameObjectがDestroyされたタイミングでDisposeする　【RepeatUntilDestory】
            // という安全に配慮されたオペレータが用意されているので、こちらを使った方が良い場合が多い
            leftStream
                .Zip(rightStream, (l, r) => Unit.Default)
                .First()
                .RepeatUntilDestroy(gameObject)
                .SubscribeToText(resultLabel, _ => resultLabel.text += "OK\n");
        }
    }
}
