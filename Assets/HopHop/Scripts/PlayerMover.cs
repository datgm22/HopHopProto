using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class PlayerMover : MonoBehaviour
    {
        /// <summary>
        /// 最高速度
        /// </summary>
        public static float SpeedMax => 10;

        enum State
        {
            Stop,   // 静止
            Run,    // 物理演算有効
        }

        Rigidbody rb;
        State currentState;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Stop();
        }

        private void FixedUpdate()
        {
            if (currentState == State.Run)
            {
                SetVelocity(rb.velocity);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

        /// <summary>
        /// 上限付き速度設定
        /// </summary>
        /// <param name="v">設定したい速度</param>
        public void SetVelocity(Vector3 v)
        {
            float speed = Mathf.Min(v.magnitude, SpeedMax);
            if (Mathf.Approximately(speed, 0))
            {
                rb.velocity = v;
            }
            else
            {
                rb.velocity = speed * v.normalized;
            }
        }

        /// <summary>
        /// 物理座標を設定する。ついでに回転を停止する。
        /// </summary>
        /// <param name="pos">移動先の座標</param>
        public void SetPosition(Vector3 pos)
        {
            rb.position = pos;
            rb.angularVelocity = Vector3.zero;
        }

        /// <summary>
        /// 物理演算を有効にする
        /// </summary>
        public void StartRun()
        {
            currentState = State.Run;
            rb.isKinematic = false;
        }

        /// <summary>
        /// 物理演算を停止
        /// </summary>
        public void Stop()
        {
            currentState = State.Stop;
            rb.isKinematic = true;
        }
    }
}
