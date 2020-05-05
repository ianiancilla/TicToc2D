using System;
using UnityEngine;

namespace TicToc.Mechanics
{
    public class SightState : MonoBehaviour
    {
        public bool InSight { get; private set; }

        public event EventHandler<SightStateEventArgs> SightStateChanged;

        public void SetSightState(bool inSight)
        {
            if(InSight == inSight)
            {
                return;
            }

            InSight = inSight;
            SightStateChanged?.Invoke(this, new SightStateEventArgs(inSight));
        }
    }
}