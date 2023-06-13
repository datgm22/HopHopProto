using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    /// <summary>
    /// スタート位置へプレイヤーを戻す状態。ミスに該当。
    /// </summary>
    public class PlayerStateToStart : IPlayerState
    {
        /// <summary>
        /// シングルトン
        /// </summary>
        static PlayerStateToStart instance;
        public static PlayerStateToStart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ();
                }
                return instance;
            }
        }

        public void Init()
        {
            // プレイヤーの速度0にして、スタート座標へ戻す
            PlayerController.Instance.Mover.SetVelocity(Vector3.zero);
            PlayerController.Instance.Mover.SetPosition(PlayerController.Instance.StartPosition);

            // TODO: 操作可能状態へ戻す
            PlayerController.Instance.ChangeState(
                PlayerController.Instance.StateWaitStart);
        }

        public void UpdateState()
        {
        }

        public void Terminate()
        {
        }
    }
}
