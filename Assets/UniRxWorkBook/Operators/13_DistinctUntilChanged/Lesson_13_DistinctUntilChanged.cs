using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace UniRxWorkBook
{
    public class Lesson_13_DistinctUntilChanged : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Text statusText;

        private void Start()
        {
            var controller = target.GetComponent<CharacterController>();

            /*
            オブジェクトが着地または離陸したタイミングを検知して画面に表示したい
            ____を書き換え、isGroundedが true→false または false→true に変わった瞬間を検知させてみよう
            */

            this.UpdateAsObservable()
                .Select(_ => controller.isGrounded)
                ._____()
                .Subscribe(isGrounded => StatusOutput(isGrounded));
        }

        #region Output
        private List<string> logList = new List<string>();
        private int maxLogLength = 20;

        /// <summary>
        /// 着地または離陸の状態を文字列に変換して表示する
        /// </summary>
        /// <param name="isGrounded"></param>
        private void StatusOutput(bool isGrounded)
        {
            var text = isGrounded ? "OnGrounded\n" : "OnJumped\n";
            AddLog(text);
            statusText.text = logList.Aggregate((p, c) => p + c);
        }

        /// <summary>
        /// ログのテキストを先頭に追加する
        /// </summary>
        private void AddLog(string text)
        {
            logList.Insert(0,text);
            if (logList.Count > maxLogLength) logList.RemoveAt(maxLogLength);
        }
        #endregion
    }
}