using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicToc.Mechanics
{
    public class SightCone : MonoBehaviour
    {
        [SerializeField]
        private float sightRange = 15;

        [SerializeField]
        private float sightAngle = 30;

        [SerializeField]
        private int sections = 30;

        private List<Vector3> rayDirections = new List<Vector3>();
        private float rayAngle;

        private List<SightState> objectsInSight = new List<SightState>();

        private void Start()
        {
            rayAngle = sightAngle / sections;
        }

        private void Update()
        {
            rayDirections.Clear();
            float startingAngle = -sightAngle / 2;

            for (int i = 0; i <= sections; ++i)
            {
                rayDirections.Add(Quaternion.AngleAxis(startingAngle + i * rayAngle, transform.right) * transform.forward);
            }

            RaycastHit2D hit;
            List<SightState> hitObjects = new List<SightState>();

            foreach (Vector3 direction in rayDirections)
            {
                float distance = sightRange;

                if (hit = Physics2D.Raycast(transform.position, direction, sightRange, 1 << 9))
                {
                    distance = hit.distance;
                }

                Debug.DrawRay(transform.position, direction * distance, Color.yellow, Time.deltaTime);

                foreach (RaycastHit2D raycastHit in Physics2D.RaycastAll(transform.position, direction, distance))
                {
                    SightState sightState = raycastHit.collider.gameObject.GetComponent<SightState>();
                    if (sightState != null)
                    {
                        sightState.SetSightState(true);
                        hitObjects.Add(sightState);
                    }
                }

                IEnumerable<SightState> lostSight = objectsInSight.Where(sightState => hitObjects.Contains(sightState) == false);
                foreach (SightState sightState in lostSight)
                {
                    sightState.SetSightState(false);
                }

                objectsInSight = hitObjects;
            }
        }
    }
}