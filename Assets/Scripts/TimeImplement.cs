using UnityEngine;

namespace TicToc.Mechanics
{
    [RequireComponent(typeof(SightState))]
    public class TimeImplement : TimeScaleObject
    {
        private void Start()
        {
            SightState sightState = GetComponent<SightState>();
            sightState.SightStateChanged += OnSightStateChanged;
            SetActive(sightState.InSight);
        }

        private void OnSightStateChanged(object sender, SightStateEventArgs e)
        {
            SetActive(e.InSight);
        }
    }
}
