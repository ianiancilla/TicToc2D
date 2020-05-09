﻿using System.Collections.Generic;
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
        private List<Vector3> vertices;

        private List<SightState> objectsInSight = new List<SightState>();

        private Mesh mesh;

        private void Start()
        {
            rayAngle = sightAngle / sections;
            mesh = new Mesh();
            mesh.name = "SightCone";
            GetComponent<MeshFilter>().mesh = mesh;
        }

        private void Update()
        {
            rayDirections.Clear();
            float startingAngle = -sightAngle / 2;

            for (int i = 0; i <= sections; ++i)
            {
                rayDirections.Add(Quaternion.AngleAxis(startingAngle + i * rayAngle, transform.right) * transform.forward);
            }

            vertices = new List<Vector3>() { transform.localPosition };

            IEnumerable<SightState> seenObjects = GetObjectsInSight(rayDirections, sightRange);

            foreach(SightState seenObject in GetObjectsInSight(rayDirections, sightRange))
            {
                seenObject.SetSightState(true);
            }

            foreach(SightState sightState in objectsInSight)
            {
                if (seenObjects.Contains(sightState) == false)
                {
                    sightState.SetSightState(false);
                }
            }

            objectsInSight = new List<SightState>(seenObjects);

            UpdateMesh();
        }

        private IEnumerable<SightState> GetObjectsInSight(IEnumerable<Vector3> directions, float sightRange)
        {
            List<SightState> hitObjects = new List<SightState>();
            RaycastHit2D hit;
            foreach (Vector3 direction in directions)
            {
                float distance = sightRange;

                if (hit = Physics2D.Raycast(transform.position, direction, sightRange, 1 << 9))
                {
                    distance = hit.distance;
                }

                // TODO get out of here
                vertices.Add(transform.InverseTransformPoint(transform.position + (direction * distance)));

                foreach (RaycastHit2D raycastHit in Physics2D.RaycastAll(transform.position, direction, distance))
                {
                    SightState sightState = raycastHit.collider.gameObject.GetComponent<SightState>();
                    if (sightState != null)
                    {
                        if (hitObjects.Contains(sightState) == false)
                        {
                            hitObjects.Add(sightState);
                        }
                    }
                }
            }

            return hitObjects;
        }

        private void UpdateMesh()
        {
            bool isBackwards = Quaternion.Angle(transform.rotation, transform.parent.rotation) > 90;
            mesh.vertices = vertices.ToArray();

            List<int> triangles = new List<int>();

            for (int i = 1; i < vertices.Count() - 1; ++i)
            {
                triangles.Add(0);
                if (isBackwards)
                {
                    triangles.Add(i + 1);
                    triangles.Add(i);
                }
                else
                {
                    triangles.Add(i);
                    triangles.Add(i + 1);
                }
            }

            mesh.triangles = triangles.ToArray();
        }
    }
}