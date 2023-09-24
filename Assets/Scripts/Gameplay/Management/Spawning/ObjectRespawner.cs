using Gameplay.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Management
{
    public class ObjectRespawner : MonoBehaviour
    {
        private static readonly UnityEvent<GridOccupier, float> OnAddObjectToRespawnInTime = new();

        private List<OccupierToRespawn> objectsToRespawn = new();

        public static void InvokeAddObjectToRespawn(GridOccupier gridOccupierToRespawn, float timeToRespawn) =>
            OnAddObjectToRespawnInTime.Invoke(gridOccupierToRespawn, timeToRespawn);

        private void OnEnable()
        {
            OnAddObjectToRespawnInTime.AddListener(AddObjectToRespawn);
            TimeUpdater.OnTimeTick.AddListener(CheckObjectsToRespawn);
        }

        private void OnDisable()
        {
            OnAddObjectToRespawnInTime.RemoveListener(AddObjectToRespawn);
            TimeUpdater.OnTimeTick.RemoveListener(CheckObjectsToRespawn);
        }

        private void AddObjectToRespawn(GridOccupier gameObject, float timeToRespawn)
        {
            var objectToRespawn = new OccupierToRespawn(gameObject, timeToRespawn);

            objectsToRespawn.Add(objectToRespawn);
        }

        private void CheckObjectsToRespawn()
        {
            if (objectsToRespawn.Count == 0) return;

            for (int i = objectsToRespawn.Count - 1; i >= 0; i--)
            {
                var obj = objectsToRespawn[i];

                if (obj.IsTimeToRespawn)
                {
                    obj.Respawn();
                    objectsToRespawn.RemoveAt(i);
                }
            }
        }
    }
}