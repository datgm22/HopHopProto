using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class Indicator : MonoBehaviour, IPlayerState
    {
        [Tooltip("マウスの操作とインジケーターの長さの対応"), SerializeField]
        float mouseToLength = 0.75f;
        [Tooltip("長さから速度への変換率"), SerializeField]
        float lengthToVelocity = 1f;
        [Tooltip("インジケーターの最大長さ"), SerializeField]
        float barMax = 4f;
        [Tooltip("無視する距離"), SerializeField]
        float ignoreLength = 0.05f;

        enum State {
            None = -1,
            WaitButton,     // ボタンの入力待ち
            Drag,           // ドラッグ中
        }

        State currentState = State.None;

        /// <summary>
        /// ドラッグしてからの移動量
        /// </summary>
        Vector2 deltaMove;

        Transform indicatorBar;

        PlayerStateUncontrol stateUncontrol = new();

        void Awake()
        {
            indicatorBar = transform.GetChild(0);
            indicatorBar.gameObject.SetActive(false);
        }

        public void Init()
        {
            PlayerController.Instance.Mover.StartRun();
            currentState = State.WaitButton;
        }

        public void Terminate()
        {
            currentState = State.None;
            indicatorBar.gameObject.SetActive(false);
        }

        public void UpdateState()
        {
            switch (currentState)
            {
                case State.WaitButton:
                    UpdateWaitButton();
                    break;

                    case State.Drag:
                    UpdateDrag();
                    break;
            }
        }

        /// <summary>
        /// マウスのボタンが押されるのを待つ
        /// </summary>
        void UpdateWaitButton()
        {
            if (!Input.GetButtonDown("Fire1"))
            {
                return;
            }

            // ドラッグ開始
            deltaMove = Vector2.zero;
            currentState = State.Drag;
            UpdateIndicator(0);
            indicatorBar.gameObject.SetActive(true);
        }

        /// <summary>
        /// マウスが離されるまでマウスの移動距離の更新
        /// </summary>
        void UpdateDrag()
        {
            float len = mouseToLength * deltaMove.magnitude;
            float clipedLength = Mathf.Min(len, barMax);

            // ショット
            if (Input.GetButtonUp("Fire1"))
            {
                Flip(clipedLength);
            }
            else
            {
                // 長さ更新
                deltaMove.x += mouseToLength * Input.GetAxis("Mouse X");
                deltaMove.y += mouseToLength * Input.GetAxis("Mouse Y");
                UpdateIndicator(clipedLength);
            }
        }

        /// <summary>
        /// プレイヤーを移動
        /// </summary>
        void Flip(float clipedLength)
        {
            if (clipedLength >= ignoreLength)
            {
                Vector2 v = lengthToVelocity * clipedLength * deltaMove.normalized;
                PlayerController.Instance.Mover.SetVelocity(-v);
            }

            // プレイヤーの状態を跳ね返り待ちへ変更
            PlayerController.Instance.ChangeState(stateUncontrol, true);
        }

        /// <summary>
        /// インジケーターの大きさを現在のdeltaMoveと最大長さによって調整
        /// </summary>
        void UpdateIndicator(float clipedLength)
        {
            var sc = transform.localScale;
            if (clipedLength < ignoreLength)
            {
                // 長さが足りないならなしにする
                sc.x = 0;
                transform.localScale = sc;
                return;
            }

            // 長さ設定
            sc.x = clipedLength;
            transform.localScale = sc;

            // 角度を求める
            //// Mathf.atan2() yとxを与えると、右方向からの角度がラジアンで返す
            //// Vector2.SignedAngle()は、2つのベクトルがなす角を度数(Degree)で返す
            float rad = Mathf.Atan2(deltaMove.y, deltaMove.x);
            var euler = transform.localEulerAngles;
            euler.z = 180f+Mathf.Rad2Deg * rad;
            transform.localEulerAngles = euler;
        }
    }
}
