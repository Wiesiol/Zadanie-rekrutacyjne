using Gameplay.Objects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Management
{
    public class ObjectsSpawner : MonoBehaviour
    {
        public static readonly UnityEvent<int> OnObjectsAmountSpawn = new();

        [SerializeField] private HealthManager objectPrefab;
        [SerializeField] private float additionalGridPercentage;

        private PoolSpawner<HealthManager> poolSpawner;
        private List<HealthManager> objects = new();

        private void Awake()
        {
            poolSpawner = new(objects, objectPrefab);
        }

        private void OnEnable()
        {
            OnObjectsAmountSpawn.AddListener(Spawn);
        }

        private void OnDisable()
        {
            OnObjectsAmountSpawn.RemoveListener(Spawn);
        }

        private void Spawn(int amount)
        {
            GenerateGrid(amount);

            for (int i = 0; i < amount; i++)
            {
                var obj = poolSpawner.Spawn(Vector3.zero, Quaternion.identity, transform);
                obj.ResetHealth();
                GridSpawner.OnConnectObjectToCell.Invoke(obj.GridOccupier);
            }
        }

        private void GenerateGrid(int gridCells)
        {
            float gridCellsWithAdditionalPercentage = gridCells + (gridCells * (additionalGridPercentage / 100));

            GridSpawner.OnNewGridSpawn.Invoke((int)gridCellsWithAdditionalPercentage);
            Debug.Log(gridCellsWithAdditionalPercentage);
        }
    }
}