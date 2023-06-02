using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopHop
{
    public class Indicator : MonoBehaviour, IPlayerState
    {
        public void Init()
        {
            PlayerController.Instance.Mover.StartRun();
        }

        public void Terminate()
        {
        }

        public void UpdateState()
        {
        }
    }
}
