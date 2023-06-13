using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    /// <summary>
    /// ミスギミック
    /// </summary>
    public class MissGimmick : MonoBehaviour, IGimmick
    {
        public IPlayerState Hit(PlayerController pcon)
        {
            return PlayerStateToStart.Instance;
        }
    }
}
