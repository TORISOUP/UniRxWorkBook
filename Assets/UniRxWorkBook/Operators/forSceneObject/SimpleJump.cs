using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;

namespace UniRxWorkBook
{
    public class SimpleJump : MonoBehaviour
    {
        [SerializeField] private float _jumpPower = 5;

        private void Start()
        {
            var controller = GetComponent<CharacterController>();
            //クリックまたはスペースキーでジャンプ
            this.UpdateAsObservable()
                .Where(_ => controller.isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
                .Subscribe(_ =>
                {
                    controller.Move(_jumpPower*Vector3.up*Time.deltaTime);
                });

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var currentY = controller.velocity.y;
                    var nextYSpeed = currentY + Physics.gravity.y*Time.deltaTime;
                    controller.Move(new Vector3(0, nextYSpeed, 0)*Time.deltaTime);
                });
        }
    }
}