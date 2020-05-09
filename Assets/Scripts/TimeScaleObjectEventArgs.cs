using System;

namespace TicToc.Mechanics
{
    public class TimeScaleObjectEventArgs : EventArgs
    {
        public readonly float TimeScale;

        public TimeScaleObjectEventArgs(float timeScale)
        {
            TimeScale = timeScale;
        }
    }
}