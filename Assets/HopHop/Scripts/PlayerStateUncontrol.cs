using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class PlayerStateUncontrol : IPlayerState
    {
        public void Init()
        {
            Debug.Log($"Start Uncontrol");
        }

        public void Update()
        {
            Debug.Log($"Update Uncontrol");
        }

        public void Terminate()
        {
            Debug.Log($"Terminate Uncontrol");
        }
    }
}
