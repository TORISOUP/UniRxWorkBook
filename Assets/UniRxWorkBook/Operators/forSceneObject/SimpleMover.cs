using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
namespace UniRxWorkBook
{
    public class SimpleMover : MonoBehaviour
    {
        [SerializeField] private float speed = 10;

        private void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    this.transform.position += new Vector3(speed, 0,0) * Time.deltaTime;
                });
        }
    }
}