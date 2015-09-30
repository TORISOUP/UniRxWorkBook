using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Operators
{

    public class Lesson_11_ThrottleFirst : MonoBehaviour
    {
        [SerializeField] private GameObject bulletObject;

        private void Start()
        {
            // 下記スクリプトは「左クリックをしている間弾を発射する」スクリプトである
            // ただしこのままでは毎フレーム弾が生成されてしまう
            // そこで、____を書き換えて「左クリックをしている間100msに１回ずつ弾を発射する」という動作にしよう
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButton(0))
                ._____()
                .Subscribe(_ =>
                {
                    var b = Instantiate(bulletObject, transform.position, Quaternion.identity) as GameObject;
                    Destroy(b, 2.0f);
                });
        }

    }
}