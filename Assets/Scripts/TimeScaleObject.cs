using System;
using UnityEngine;

namespace TicToc.Mechanics
{
    public class TimeScaleObject : MonoBehaviour
    {
        [SerializeField]
        protected float timeScale = 1;
        public float TimeScale { get { return timeScale; } protected set { timeScale = value; } }
        public bool IsActive { get; protected set; }

        public event EventHandler<TimeScaleObjectEventArgs> TimeScaleChanged;
        public event EventHandler<TimeScaleObjectEventArgs> TimeScaleObjectActivated;
        public event EventHandler<TimeScaleObjectEventArgs> TimeScaleObjectDeactivated;

        protected virtual void OnEnable()
        {
            TimeManager.Instance.Register(this);
        }

        protected virtual void OnDisable()
        {
            TimeManager.Instance.Deregister(this);
        }

        public virtual void SetTimeScale(float timeScale)
        {
            if (Mathf.Abs(TimeScale - timeScale) > float.Epsilon)
            {
                TimeScale = timeScale;
                TimeScaleChanged?.Invoke(this, new TimeScaleObjectEventArgs(TimeScale));
            }
        }

        public virtual void SetActive(bool isActive)
        {
            if(isActive == IsActive)
            {
                return;
            }

            IsActive = isActive;

            if (IsActive)
            {
                TimeScaleObjectActivated?.Invoke(this, new TimeScaleObjectEventArgs(TimeScale));
            }
            else
            {
                TimeScaleObjectDeactivated?.Invoke(this, new TimeScaleObjectEventArgs(TimeScale));
            }
        }
    }
}