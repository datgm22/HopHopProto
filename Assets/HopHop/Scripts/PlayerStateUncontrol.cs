using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class PlayerStateUncontrol : IPlayerState
    {
        // 操作を無視する秒数
        static float IgnoreSeconds => 0.1f;

        // 開始から操作を無視する秒数のカウンタ
        float ignoreTime;

        public void Init()
        {
            PlayerController.Instance.onCollisionWall.AddListener(OnCollisionWall);
            ignoreTime = 0;
        }

        public void UpdateState()
        {
            ignoreTime += Time.deltaTime;
        }

        public void Terminate()
        {
            PlayerController.Instance.onCollisionWall.RemoveListener(OnCollisionWall);
        }

        /// <summary>
        /// 壁に接触した時に、ゲーム継続可能な状態ならプレイ状態へ移行
        /// </summary>
        void OnCollisionWall()
        {
            if (    (ignoreTime >= IgnoreSeconds)
                &&  (GameBehaviour.Instance.CurrentState == GameBehaviour.State.Play))
            {
                // プレイヤーの状態を跳ね返り待ちへ変更
                PlayerController.Instance.ChangeState(PlayerController.Instance.IndicatorInstance);
            }
        }
    }
}
