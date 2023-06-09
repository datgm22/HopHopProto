using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public interface IPlayerState
    {
        void Init();
        void UpdateState();
        void Terminate();
    }
}
