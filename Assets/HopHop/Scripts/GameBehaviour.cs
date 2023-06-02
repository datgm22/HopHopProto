using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class GameBehaviour : MonoBehaviour
    {
        public static GameBehaviour Instance { get;private set; }

        public enum State
        {
            None = -1,
            WaitStart,  // フェードが終わるのを待つ
            Play,       // 操作可能状態
        }

        public State CurrentState { get; private set; }
        State nextState = State.WaitStart;

        /// <summary>
        /// 状態秒数
        /// </summary>
        float stateTime;

        private void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            ChangeState();
            UpdateState();
        }

        /// <summary>
        /// nextStateが設定されていた時に、次の状態へきりかえる　
        /// </summary>
        void ChangeState()
        {
            if (nextState == State.None) return;

            CurrentState = nextState;
            nextState = State.None;

            stateTime = 0;
            Debug.Log($"Change State {CurrentState}");
        }

        /// <summary>
        /// 状態の更新
        /// </summary>
        void UpdateState()
        {
            stateTime += Time.deltaTime;

            switch (CurrentState)
            {
                case State.WaitStart:
                    UpdateWaitStart();
                    break;
            }
        }

        /// <summary>
        /// 開始まち
        /// </summary>
        private void UpdateWaitStart()
        {
            // フェード処理や開始演出ができたらそれに変更
            // 仮で1秒待ちにしておく

            if (stateTime >= 1)
            {
                ChangeState(State.Play, true);
            }
        }

        /// <summary>
        /// 状態切り替えの要求。
        /// デフォルトでは、nextStateが設定済みの時は
        /// 登録を拒否する。
        /// forceにtrueを指定するとnextStateを上書きする。
        /// </summary>
        /// <param name="state">切り替えたい状態</param>
        /// <param name="force">強制的に設定したい時true</param>
        public void ChangeState(State state, bool force=false)
        {
            if (force || (nextState == State.None))
            {
                nextState = state;
            }
        }
    }
}
