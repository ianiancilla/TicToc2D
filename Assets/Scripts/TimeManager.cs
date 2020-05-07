using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicToc.Mechanics
{
    public class TimeManager : MonoBehaviour
    {
        private static TimeManager instance;
        public static TimeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(TimeManager).Name).AddComponent<TimeManager>();
                }

                return instance;
            }
        }

        public event EventHandler<TimeScaleObjectEventArgs> TimeScaleChanged;
        public float TimeScale { get; private set; }
        private List<TimeScaleObject> timeScaleObjects = new List<TimeScaleObject>();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }

            instance = this;
        }

        private void UpdateTimeScale()
        {
            IEnumerable<TimeScaleObject> activeTimeScaleObjects = timeScaleObjects.Where(timeObject => timeObject.IsActive);

            if(activeTimeScaleObjects.Count() == 0)
            {
                SetTimeScale(0);
            }
            else
            {
                SetTimeScale(activeTimeScaleObjects.Max(timeScaleObject => timeScaleObject.TimeScale));
            }
        }

        private void SetTimeScale(float timeScale)
        {
            if (Mathf.Abs(TimeScale - timeScale) > float.Epsilon)
            {
                TimeScale = timeScale;
                TimeScaleChanged?.Invoke(this, new TimeScaleObjectEventArgs(TimeScale));
            }
        }

        public void Register(TimeScaleObject timeScaleObject)
        {
            timeScaleObject.TimeScaleChanged += OnTimeScaleObjectEvent;
            timeScaleObject.TimeScaleObjectActivated += OnTimeScaleObjectEvent;
            timeScaleObject.TimeScaleObjectDeactivated += OnTimeScaleObjectEvent;
            timeScaleObjects.Add(timeScaleObject);
            UpdateTimeScale();
        }

        public void Deregister(TimeScaleObject timeScaleObject)
        {
            timeScaleObject.TimeScaleChanged -= OnTimeScaleObjectEvent;
            timeScaleObject.TimeScaleObjectActivated -= OnTimeScaleObjectEvent;
            timeScaleObject.TimeScaleObjectDeactivated -= OnTimeScaleObjectEvent;
            timeScaleObjects.Remove(timeScaleObject);
            UpdateTimeScale();
        }

        private void OnTimeScaleObjectEvent(object sender, TimeScaleObjectEventArgs e)
        {
            UpdateTimeScale();
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}