using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    /// <summary>
    /// GameBehaviourがPlayになるのを待って、プレイヤーを開始状態にする
    /// </summary>
    public class PlayerStateWaitStart : IPlayerState
    {
        public void Init() { }

        public void UpdateState()
        {
            if (GameBehaviour.Instance.CurrentState == GameBehaviour.State.Play)
            {
                // Playになった
                PlayerController.Instance.ChangeState(PlayerController.Instance.IndicatorInstance, true);
            }
        }

        public void Terminate() { }
    }
}
