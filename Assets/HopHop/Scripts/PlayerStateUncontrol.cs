using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class PlayerStateUncontrol : IPlayerState
    {
        public void Init()
        {
            PlayerController.Instance.onCollisionWall.AddListener(OnCollisionWall);
        }

        public void UpdateState() { }

        public void Terminate()
        {
            PlayerController.Instance.onCollisionWall.RemoveListener(OnCollisionWall);
        }

        /// <summary>
        /// 壁に接触した時に、ゲーム継続可能な状態ならプレイ状態へ移行
        /// </summary>
        void OnCollisionWall()
        {
            if (GameBehaviour.Instance.CurrentState == GameBehaviour.State.Play)
            {
                // プレイヤーの状態を跳ね返り待ちへ変更
                PlayerController.Instance.ChangeState(PlayerController.Instance.IndicatorInstance);
            }
        }
    }
}
