using UnityEngine;

namespace TicToc.Mechanics
{
    [RequireComponent(typeof(SightState))]
    public class TestSightState : MonoBehaviour
    {
        SightState sightState;

        private void Start()
        {
            sightState = GetComponent<SightState>();
            VisualizeState(false);
            sightState.SightStateChanged += OnSightStateChanged;
        }

        private void OnSightStateChanged(object sender, SightStateEventArgs e)
        {
            VisualizeState(e.InSight);
        }

        private void VisualizeState(bool state)
        {
            if(state)
            {
                Debug.LogFormat("{0} is in sight!", gameObject.name);
            }
            else
            {
                Debug.LogFormat("{0} is out of sight!", gameObject.name);
            }
        }
    }
}